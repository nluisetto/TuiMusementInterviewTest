namespace TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Domain;

public interface ICityRepository
{
    public Task<IEnumerable<City>> All();
}
