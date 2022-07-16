using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.WeatherApi.DTO;

namespace TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.WeatherApi;

public interface IWeatherApiClient
{
    public Task<Forecast> GetForecast(decimal latitude, decimal longitude, int numberOfDays);
}
