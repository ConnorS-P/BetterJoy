namespace BetterJoy.Config;

public sealed record MainFormConfigSettings : SettingsFromFile
{
    public bool AllowCalibration { get; init; } = true;

    public override string ConfigSection => "MainForm";
}
