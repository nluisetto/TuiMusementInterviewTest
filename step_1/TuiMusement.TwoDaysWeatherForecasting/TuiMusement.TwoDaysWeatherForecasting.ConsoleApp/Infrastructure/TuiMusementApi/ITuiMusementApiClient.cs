using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.TuiMusementApi.DTO;

namespace TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.TuiMusementApi;

public interface ITuiMusementApiClient
{
    public Task<IEnumerable<City>> GetAllCities();
}
