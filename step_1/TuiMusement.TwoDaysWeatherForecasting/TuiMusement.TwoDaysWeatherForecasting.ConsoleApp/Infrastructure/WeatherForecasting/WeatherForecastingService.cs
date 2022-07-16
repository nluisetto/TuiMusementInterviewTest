using System.Collections.Immutable;
using AutoMapper;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Domain;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.WeatherApi;

namespace TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.WeatherForecasting;

public class WeatherForecastingService : IWeatherForecastingService
{
    private readonly IMapper _mapper;
    private readonly IWeatherApiClient _weatherApiClient;

    public WeatherForecastingService(IMapper mapper, IWeatherApiClient weatherApiClient)
    {
        _mapper = mapper;
        _weatherApiClient = weatherApiClient;
    }

    public async Task<IEnumerable<WeatherForecast>> Forecast(City city, int numberOfDays)
    {
        var forecast = await _weatherApiClient.GetForecast(city.Latitude, city.Longitude, numberOfDays);

        if (forecast.ForecastDay == null)
            return ImmutableList<WeatherForecast>.Empty;

        return _mapper.Map<IEnumerable<WeatherForecast>>(forecast.ForecastDay);
    }
}
