using Microsoft.Extensions.Configuration;
using System;

namespace BetterJoy.Config;

public static class CoreConfig
{
    public static readonly IConfigurationRoot ConfigRoot = new ConfigurationBuilder()
        .SetBasePath(AppContext.BaseDirectory)
        .AddJsonFile("appsettings.jsonc", optional: false, reloadOnChange: true)
        .Build();
}
