using FakeItEasy;
using Microsoft.Extensions.Logging;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Application.Common;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Application.Features.TwoDaysWeatherForecasting;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Domain;
using Xunit;

namespace TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Tests.Application.Features;

public class TwoDaysWeatherForecastingServiceTest
{
    [Fact]
    public async void Execute_Success()
    {
        // Setting-up a fake ILogger
        var logger = A.Fake<ILogger<TwoDaysWeatherForecastingService>>();

        // Setting-up a fake CityRepository that return two Dummy City
        var cities = A.CollectionOfDummy<City>(2);
        var cityRepository = A.Fake<ICityRepository>();
        A.CallTo(() => cityRepository.All()).Returns(cities);

        // Setting-up a fake ForecastingService that return specific WeatherForecast List for each City returned by CityRepository
        var forecasts = new List<IList<WeatherForecast>>
        {
            A.CollectionOfDummy<WeatherForecast>(2),
            A.CollectionOfDummy<WeatherForecast>(2)
        };
        var forecastingService = A.Fake<IWeatherForecastingService>();
        A.CallTo(() => forecastingService.Forecast(cities.First(), 2)).Returns(forecasts.First());
        A.CallTo(() => forecastingService.Forecast(cities.Skip(1).First(), 2)).Returns(forecasts.Skip(1).First());
        
        // Setting-up a fake ProgressNotifier
        var progressNotifier = A.Fake<IProgressNotifier>();

        // Executing the test method
        var featureService = new TwoDaysWeatherForecastingService(logger, cityRepository, forecastingService, progressNotifier);
        await featureService.Execute();

        // Asserting that cityRepository.All have been called once
        A.CallTo(() => cityRepository.All()).MustHaveHappenedOnceExactly();
        
        // Asserting that forecastingService.Forecast have been called for the first city
        A.CallTo(() => forecastingService.Forecast(cities.First(), 2)).MustHaveHappenedOnceExactly();
        
        // Asserting that forecastingService.Forecast have been called for the second city
        A.CallTo(() => forecastingService.Forecast(cities.Skip(1).First(), 2)).MustHaveHappenedOnceExactly();
        
        // Asserting that progressNotifier.Notify have been called one time for each of the cities returned by cityRepository.All
        A.CallTo(() => progressNotifier.Notify(A<string>.Ignored)).MustHaveHappened(cities.Count, Times.Exactly);
    }
    
    [Fact]
    public async void Execute_RetrieveCities_Fail_PropagateException()
    {
        // Setting-up a fake ILogger
        var logger = A.Fake<ILogger<TwoDaysWeatherForecastingService>>();

        // Setting-up a fake CityRepository that throws an Exception
        var exception = new Exception("An exception message");
        var cityRepository = A.Fake<ICityRepository>();
        A.CallTo(() => cityRepository.All()).Throws(exception);
        
        // Setting-up a fake ForecastingService
        var forecastingService = A.Fake<IWeatherForecastingService>();
        
        // Setting-up a fake ProgressNotifier
        var progressNotifier = A.Fake<IProgressNotifier>();

        // Executing the test method capturing the Exception
        var featureService = new TwoDaysWeatherForecastingService(logger, cityRepository, forecastingService, progressNotifier);
        var thrownException = await Record.ExceptionAsync(async () => await featureService.Execute());
        
        // Asserting the captured Exception is the same thrown by CityRepository
        Assert.Same(exception, thrownException);

        // Asserting that cityRepository.All have been called once
        A.CallTo(() => cityRepository.All()).MustHaveHappenedOnceExactly();
        
        // Asserting that forecastingService.Forecast have never been called
        A.CallTo(() => forecastingService.Forecast(A<City>.Ignored, A<int>.Ignored)).MustNotHaveHappened();
        
        // Asserting that progressNotifier.Notify have never been called
        A.CallTo(() => progressNotifier.Notify(A<string>.Ignored)).MustNotHaveHappened();
    }
    
    [Fact]
    public async void Execute_Forecast_Fail_PropagateException()
    {
        // Setting-up a fake ILogger
        var logger = A.Fake<ILogger<TwoDaysWeatherForecastingService>>();
        
        // Setting-up a fake CityRepository that return two Dummy City
        var cities = A.CollectionOfDummy<City>(2);;
        var cityRepository = A.Fake<ICityRepository>();
        A.CallTo(() => cityRepository.All()).Returns(cities);

        // Setting-up a ForecastingService Fake that throws an Exception
        var exception = new Exception("An exception message");
        var forecastingService = A.Fake<IWeatherForecastingService>();
        A.CallTo(() => forecastingService.Forecast(A<City>.Ignored, A<int>.Ignored)).Throws(exception);

        // Setting-up a fake ProgressNotifier
        var progressNotifier = A.Fake<IProgressNotifier>();

        // Executing the test method capturing the Exception
        var featureService = new TwoDaysWeatherForecastingService(logger, cityRepository, forecastingService, progressNotifier);
        var thrownException = await Record.ExceptionAsync(async () => await featureService.Execute());
        
        // Asserting the captured Exception is an AggregateException
        Assert.IsType<AggregateException>(thrownException);
        
        // Asserting that the captured AggregateException contains the Exception thrown by forecastingService.Forecast
        Assert.Contains(exception, (thrownException as AggregateException)!.InnerExceptions);

        // Asserting that cityRepository.All have been called once
        A.CallTo(() => cityRepository.All()).MustHaveHappenedOnceExactly();
        
        // Asserting that forecastingService.Forecast have been called once
        A.CallTo(() => forecastingService.Forecast(A<City>.Ignored, A<int>.Ignored)).MustHaveHappenedOnceOrMore();

        // Asserting that progressNotifier.Notify have never been called
        A.CallTo(() => progressNotifier.Notify(A<string>.Ignored)).MustNotHaveHappened();
    }
}
