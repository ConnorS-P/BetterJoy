using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            _settings = CoreConfig.ConfigRoot.GetRequiredSection(_settings.ConfigSection)
                .Get<T>(o =>
                    o.ErrorOnUnknownConfiguration = ShowErrors)!;
        }
        catch (Exception ex)
        {
            if (ShowErrors)
            {
                var tempSettings = CoreConfig.ConfigRoot.GetRequiredSection(_settings.ConfigSection).Get<T>()!;

                logger?.Log($"Update error for {typeof(T).Name}: {ex.Message}", Logger.LogLevel.Error);

                var results = new List<ValidationResult>();
                if (!Validator.TryValidateObject(tempSettings, new ValidationContext(tempSettings), results, true))
                {
                    foreach (var r in results)
                    {
                        logger?.Log($"Error for value: {r.ErrorMessage}", Logger.LogLevel.Error);
                    }
                }
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
