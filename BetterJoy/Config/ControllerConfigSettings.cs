using BetterJoy.Controller;
using System.Collections.Immutable;

namespace BetterJoy.Config;

public sealed record ControllerConfigSettings : SettingsFromFile
{
    public int LowFreqRumble { get; init; } = 160;
    public int HighFreqRumble { get; init; } = 320;
    public bool EnableRumble { get; init; } = true;
    public bool ShowAsXInput { get; init; } = true;
    public bool ShowAsDs4 { get; init; } = false;
    public float StickLeftDeadzone { get; init; } = 0.15f;
    public float StickRightDeadzone { get; init; } = 0.15f;
    public float StickLeftRange { get; init; } = 0.90f;
    public float StickRightRange { get; init; } = 0.90f;
    public bool SticksSquared { get; init; } = false;
    public ImmutableArray<float> StickLeftAntiDeadZone { get; init; } = [0.0f, 0.0f];
    public ImmutableArray<float> StickRightAntiDeadZone { get; init; } = [0.0f, 0.0f];
    public float AHRSBeta { get; init; } = 0.05f;
    public float ShakeDelay { get; init; } = 200;
    public bool ShakeInputEnabled { get; init; } = false;
    public float ShakeSensitivity { get; init; } = 10;
    public bool ChangeOrientationDoubleClick { get; init; } = true;
    public bool DragToggle { get; init; } = false;
    public string ExtraGyroFeature { get; init; } = "none";
    public int GyroAnalogSensitivity { get; init; } = 400;
    public bool GyroAnalogSliders { get; init; } = false;
    public bool GyroHoldToggle { get; init; } = true;
    public bool GyroLeftHanded { get; init; } = false;
    public ImmutableArray<int> GyroMouseSensitivity { get; init; } = [1200, 800];
    public float GyroStickReduction { get; init; } = 1.5f;
    public ImmutableArray<float> GyroStickSensitivity { get; init; } = [40.0f, 10.0f];
    public bool HomeLongPowerOff { get; init; } = true;
    public bool HomeLEDOn { get; init; } = true;
    public long PowerOffInactivityMins { get; init; } = -1;
    public bool SwapAB { get; init; } = false;
    public bool SwapXY { get; init; } = false;
    public bool UseFilteredMotion { get; init; } = true;
    public Joycon.DebugType DebugType { get; init; } = Joycon.DebugType.None;
    public Joycon.Orientation DoNotRejoin { get; init; } = Joycon.Orientation.None;
    public bool AutoPowerOff { get; init; } = false;
    public bool AllowCalibration { get; init; } = true;
    public override string ConfigSection => "Controller";
}
