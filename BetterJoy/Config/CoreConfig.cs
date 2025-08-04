using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;

namespace BetterJoy.Config;

public static class CoreConfig
{
    private const string BaseFile = "appsettings.jsonc";
    private const string OverrideFile = "appsettings.overrides.json";

    /// <summary>Alias for backwards-compat with old code.</summary>
    public static readonly IConfigurationRoot ConfigRoot;

    // ---------------------------------------------------------------------
    // typed helpers

    public static void SetValue<TSettings, TValue>(
        Expression<Func<TSettings, TValue>> selector,
        TValue value)
        where TSettings : SettingsFromFile, new() =>
        _writable.SetAndSave(KeyPath(selector), ToConfigString(value));

    public static void ResetValue<TSettings, TValue>(
        Expression<Func<TSettings, TValue>> selector)
        where TSettings : SettingsFromFile, new()
    {
        var key = KeyPath(selector);
        if (_writable.Remove(key))
            _writable.Save();
    }

    // ---------------------------------------------------------------------
    private static readonly WritableJsonProvider _writable;
    private static readonly ConcurrentDictionary<Type, string> _sectionCache = new();

    static CoreConfig()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile(BaseFile, optional: false, reloadOnChange: true);

        // writable overrides layer
        var overrideSource = new WritableJsonConfigurationSource
        {
            Path = OverrideFile,
            Optional = true,
            ReloadOnChange = true
        };
        overrideSource.ResolveFileProvider();
        builder.Add(overrideSource);

        ConfigRoot = builder.Build();
        _writable = (WritableJsonProvider)overrideSource.Provider!;
    }

    // ---------------------------------------------------------------------
    // helpers

    private static string KeyPath<TSettings, TValue>(
        Expression<Func<TSettings, TValue>> selector)
        where TSettings : SettingsFromFile, new()
    {
        if (selector.Body is not MemberExpression { Member.MemberType: MemberTypes.Property } m)
            throw new ArgumentException("Use a simple property selector: s => s.Prop", nameof(selector));

        var section = _sectionCache.GetOrAdd(typeof(TSettings), _ => new TSettings().ConfigSection);
        return $"{section}:{m.Member.Name}";
    }

    private static string ToConfigString(object? value)
    {
        // For primitives this gives 160, true, "text" etc.
        // For complex values (arrays/objects) we keep full JSON.
        var json = JsonSerializer.Serialize(value, value?.GetType() ?? typeof(object));
        if (value is string or char or bool or byte or sbyte or short or ushort or
            int or uint or long or ulong or float or double or decimal)
        {
            // Strip quotes that JsonSerializer adds around strings/chars.
            return json.Trim('"').ToLowerInvariant();
        }
        return json;
    }

    // ---------------------------------------------------------------------
    // custom source + provider

    private sealed class WritableJsonConfigurationSource : JsonConfigurationSource
    {
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            Provider = new WritableJsonProvider(this);
            return Provider;
        }

        public IConfigurationProvider? Provider { get; private set; }
    }

    private sealed class WritableJsonProvider : JsonConfigurationProvider
    {
        public WritableJsonProvider(JsonConfigurationSource src) : base(src) { }

        public void SetAndSave(string key, string value)
        {
            Data[key] = value;
            Save();
        }

        public bool Remove(string key) => Data.Remove(key);

        public void Save()
        {
            var filePath = Source.FileProvider!.GetFileInfo(Source.Path!).PhysicalPath!;
            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

            var json = JsonSerializer.Serialize(Data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);

            OnReload();            // signal change to listeners
        }
    }
}
