namespace TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.WeatherApi.DTO;

public class Forecast
{
    public IReadOnlyList<ForecastDay>? ForecastDay { get; set; }
}
