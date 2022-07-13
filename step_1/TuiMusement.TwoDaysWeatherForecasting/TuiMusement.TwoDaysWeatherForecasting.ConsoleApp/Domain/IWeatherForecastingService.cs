namespace TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Domain;

public interface IWeatherForecastingService
{
    public Task<IEnumerable<WeatherForecast>> Forecast(City city, int numberOfDays);
}
