namespace TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Domain;

public class WeatherForecast
{
    public DateOnly Date { get; }
    public string WeatherCondition { get; }

    public WeatherForecast(DateOnly date, string weatherCondition)
    {
        Date = date;
        WeatherCondition = weatherCondition;
    }
}
