using System.Reflection;

namespace Hyenalerts;

public static class Constants
{
    public static string AppName => Assembly.GetExecutingAssembly().GetName().Name;
    public static string AppVersion => Assembly.GetExecutingAssembly().GetName().Version?.ToString();

    public const string ConfigPath = "data\\config.json";
    public const string OldConfigPath = "data\\config_old.json";
}
