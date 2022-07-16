namespace TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.Configuration;

[Serializable]
public class ConfigurationException : Exception
{
    public string ConfigurationKey { get; }

    public ConfigurationException(string configurationKey)
    {
        ConfigurationKey = configurationKey;
    }
}
