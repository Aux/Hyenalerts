using Hyenalerts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ZLogger;
using ZLogger.Providers;

Registry.ValidateConfig();

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(config =>
    {
        config.AddJsonFile(Constants.ConfigPath, false);
    })
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddZLoggerConsole();
        logging.SetMinimumLevel(LogLevel.Debug);
        logging.AddZLoggerRollingFile(rolling =>
        {
            rolling.FilePathSelector = (dt, x) => $"data/logs/{dt.ToLocalTime():yyyy-MM-dd}_{x:000}.log";
            rolling.RollingInterval = RollingInterval.Day;
            rolling.RollingSizeKB = 1024;
        });
    })
    .AddDiscord()
    .ConfigureServices(services =>
    {
        services.AddHttpClient();
    })
    .Build();

await host.RunAsync();
