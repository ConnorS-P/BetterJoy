namespace BetterJoy.Config;

public class ControllerConfig(Logger? logger = null) : Config<ControllerConfigSettings>(logger)
{
    public void ToggleSwapAB()
    {
        _settings = _settings with { SwapAB = !_settings.SwapAB };
    }
    
    public void ToggleSwapXY()
    {
        _settings = _settings with { SwapXY = !_settings.SwapXY };
    }
}
