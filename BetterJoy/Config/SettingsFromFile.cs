namespace BetterJoy.Config;

public abstract record SettingsFromFile
{
    public abstract string ConfigSection { get; }
}
