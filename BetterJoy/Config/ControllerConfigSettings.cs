using BetterJoy.Controller;
using BetterJoy.Hardware.Data;
using System;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace BetterJoy.Config;

public sealed record ControllerConfigSettings : SettingsFromFile
{
    [Range(41, 626)] public int LowFreqRumble { get; init; } = 160;
    [Range(82, 1252)] public int HighFreqRumble { get; init; } = 320;
    public bool EnableRumble { get; init; } = true;
    public bool ShowAsXInput { get; init; } = true;
    public bool ShowAsDs4 { get; init; } = false;
    [Range(0.0, 1.0)] public float StickLeftDeadZone { get; init; } = 0.15f;
    [Range(0.0, 1.0)] public float StickRightDeadZone { get; init; } = 0.15f;
    [Range(0.0, 1.0)] public float StickLeftRange { get; init; } = 0.90f;
    [Range(0.0, 1.0)] public float StickRightRange { get; init; } = 0.90f;
    public bool SticksSquared { get; init; } = false;
    public Float2 StickLeftAntiDeadZone { get; init; } = new(0.0f, 0.0f);
    public Float2 StickRightAntiDeadZone { get; init; } = new(0.0f, 0.0f);
    public float AHRSBeta { get; init; } = 0.05f;
    [Range(15, Int32.MaxValue)] public float ShakeInputDelay { get; init; } = 200;
    public bool ShakeInputEnabled { get; init; } = false;
    public float ShakeInputSensitivity { get; init; } = 10;
    public bool ChangeOrientationDoubleClick { get; init; } = true;
    public bool DragToggle { get; init; } = false;
    public string ExtraGyroFeature { get; init; } = "none";
    public int GyroAnalogSensitivity { get; init; } = 400;
    public bool GyroAnalogSliders { get; init; } = false;
    public bool GyroHoldToggle { get; init; } = true;
    public bool GyroLeftHanded { get; init; } = false;
    public Float2 GyroMouseSensitivity { get; init; } = new(1200.0f, 800.0f);
    public float GyroStickReduction { get; init; } = 1.5f;
    public Float2 GyroStickSensitivity { get; init; } = new(40.0f, 10.0f);
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
