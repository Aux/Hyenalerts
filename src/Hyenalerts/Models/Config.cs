namespace Hyenalerts.Models;

public class Config
{
    public string AppName = Constants.AppName; 
    public string AppVersion = Constants.AppVersion;

    public TimeSpan DefaultPollingRate { get; set; } = TimeSpan.FromMinutes(1);

    public bool StartWithTwitch { get; set; } = true;

    public DiscordConfig Discord { get; set; } = new();
    public TwitchConfig Twitch { get; set; } = new();
    public YoutubeConfig Youtube { get; set; } = new();
}

public class DiscordConfig
{
    public string Token { get; set; } = "REPLACE ME";
}

public class TwitchConfig
{
    public string ClientId { get; set; } = "REPLACE ME";
    public string ClientSecret { get; set; } = "REPLACE ME";
    public TimeSpan? PollingRateOverride { get; set; } = null;
}

public class YoutubeConfig
{
    public string Token { get; set; } = "REPLACE ME";
    public TimeSpan? PollingRateOverride { get; set; } = null;
}