using Discord;
using Discord.Interactions;
using Discord.Rest;
using Discord.WebSocket;
using Hyenalerts.Models;
using Hyenalerts.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace Hyenalerts;

public static class Registry
{
    public static void ValidateConfig()
    {
        Directory.CreateDirectory("data");

        var latestConfig = new Config();
        if (File.Exists(Constants.ConfigPath))
        {
            string currentJson = File.ReadAllText(Constants.ConfigPath);
            var currentConfig = JsonConvert.DeserializeObject<Config>(currentJson);

            if (currentConfig.AppVersion != latestConfig.AppVersion)
            {
                var oldJson = JsonConvert.SerializeObject(currentConfig, Formatting.Indented);
                File.WriteAllText(Constants.OldConfigPath, oldJson);

                var latestJson = JsonConvert.SerializeObject(latestConfig, Formatting.Indented);
                File.WriteAllText(Constants.ConfigPath, latestJson);

                Console.WriteLine($"A new configuration file has been created at:" +
                    $"\n\t{Path.Combine(AppContext.BaseDirectory, Constants.ConfigPath)}");
                Console.WriteLine("Your previous configuration file has been renamed in the same directory. Please " +
                    "confirm your authentication details and restart the application.");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                Environment.Exit(0);
            }

            Console.WriteLine($"Verified config version {latestConfig.AppVersion}");
        } else
        {
            var latestJson = JsonConvert.SerializeObject(latestConfig, Formatting.Indented);
            File.WriteAllText(Constants.ConfigPath, latestJson);

            Console.WriteLine($"A new configuration file has been created created at:\n" +
                $"\t{Path.Combine(AppContext.BaseDirectory, Constants.ConfigPath)}");
            Console.WriteLine("Please edit it with your authentication details and restart the application.");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }

    public static IHostBuilder AddDiscord(this IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddTransient<DiscordRestClient>();
            services.AddSingleton(_ => new DiscordSocketClient(new()
            {
                AlwaysDownloadUsers = false,
                GatewayIntents = GatewayIntents.None,
                LogLevel = LogSeverity.Verbose,
                MessageCacheSize = 0,
                SuppressUnknownDispatchWarnings = true
            }));
            services.AddSingleton(services =>
            {
                var discord = services.GetRequiredService<DiscordSocketClient>();
                return new InteractionService(discord, new()
                {
                    LogLevel = LogSeverity.Verbose
                });
            });

            services.AddHostedService<DiscordStartupService>();
            services.AddHostedService<InteractionHandlingService>();
        });

        return builder;
    }

    public static IHostBuilder AddYoutube(this IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {

        });

        return builder;
    }
}
