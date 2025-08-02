using System.Net;

namespace BetterJoy.Config;

public sealed record ProgramConfigSettings : SettingsFromFile
{
    public bool UseHIDHide { get; init; } = true;
    public bool HIDHideAlwaysOn { get; init; } = false;
    public bool PurgeWhitelist { get; init; } = false;
    public bool PurgeAffectedDevices { get; init; } = false;
    public bool MotionServer { get; init; } = true;
    public IPAddress IP { get; init; } = IPAddress.Loopback;
    public int Port { get; init; } = 26760;
        
    public override string ConfigSection => "Program";
}
