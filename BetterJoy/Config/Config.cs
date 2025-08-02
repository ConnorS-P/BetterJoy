using Microsoft.Extensions.Configuration;
using System;
using System.Threading;

namespace BetterJoy.Config;

public abstract class Config<T>(Logger? logger = null) where T : SettingsFromFile, new()
{
    public bool ShowErrors = true;

    protected volatile T _settings = new();

    public T Settings => _settings;

    public void Update()
    {
        try
        {
            _settings = CoreConfig.ConfigRoot.GetRequiredSection(_settings.ConfigSection).Get<T>()!;
        }
        catch (Exception ex)
        {
            if (ShowErrors)
            {
                logger?.Log($"Update error for {typeof(T).Name}: {ex.Message}", Logger.LogLevel.Error);
            }
        }
    }

    public Config<T> Clone()
    {
        var clone = (Config<T>)MemberwiseClone();
        clone._settings = _settings with { };
        return clone;
    }
}
