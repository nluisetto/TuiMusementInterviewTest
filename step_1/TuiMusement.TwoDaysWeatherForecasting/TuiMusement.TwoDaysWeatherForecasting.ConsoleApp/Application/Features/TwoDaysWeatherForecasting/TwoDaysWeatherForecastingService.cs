using System.Collections.Concurrent;
using System.Collections.Immutable;
using Microsoft.Extensions.Logging;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Application.Common;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Domain;

namespace TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Application.Features.TwoDaysWeatherForecasting;

public class TwoDaysWeatherForecastingService : ITwoDaysWeatherForecastingService
{
    private readonly ILogger<TwoDaysWeatherForecastingService> _logger;
    private readonly ICityRepository _cityRepository;
    private readonly IWeatherForecastingService _weatherForecastingService;
    private readonly IProgressNotifier _progressNotifier;
    
    public TwoDaysWeatherForecastingService(ILogger<TwoDaysWeatherForecastingService> logger, ICityRepository cityRepository, IWeatherForecastingService weatherForecastingService, IProgressNotifier progressNotifier)
    {
        _logger = logger;
        _cityRepository = cityRepository;
        _weatherForecastingService = weatherForecastingService;
        _progressNotifier = progressNotifier;
    }
    
    public async Task Execute()
    {
        _logger.LogInformation("Start {Feature} execution", nameof(TwoDaysWeatherForecastingService));
        
        var exceptions = new ConcurrentQueue<Exception>();
        var allCities = await _cityRepository.All();

        var forecastingTasks = allCities
            .AsParallel()
            .Select(RetrieveWeatherForecasts(exceptions));

        await Task.WhenAll(forecastingTasks);
        
        if (exceptions.Any())
            throw new AggregateException(exceptions);
        
        _logger.LogInformation("{Feature} execution completed", nameof(TwoDaysWeatherForecastingService));
    }
    
    private Func<City, Task> RetrieveWeatherForecasts(ConcurrentQueue<Exception> exceptions) => async (city) =>
    {
        try {
            var forecasts = await _weatherForecastingService.Forecast(city, 2);
            var sortedForecasts = forecasts.OrderBy(forecast => forecast.Date).ToImmutableList();
                
            var todayForecast = sortedForecasts.First();
            var tomorrowForecast = sortedForecasts.Skip(1).First();
                
            _progressNotifier.Notify($"Processed city {city.Name} | {todayForecast.WeatherCondition} - {tomorrowForecast.WeatherCondition}");
        }
        catch (Exception ex) {
            _logger.LogError(ex, "An error occurred while retrieving weather for {@City}", city);
            exceptions.Enqueue(ex);
        }
    };
}
