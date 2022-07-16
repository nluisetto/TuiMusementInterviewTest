using AutoFixture;
using AutoFixture.Xunit2;
using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Domain;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.WeatherApi;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.WeatherApi.DTO;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.WeatherForecasting;
using Xunit;

namespace TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Tests.Infrastructure.WeatherForecasting;

public class WeatherForecastingServiceTest
{
    private readonly Fixture _fixture;
    
    public WeatherForecastingServiceTest()
    {
        _fixture = new Fixture();
        // Instruct Autofixture to build DateOnly instances from the provided seed.
        // This is needed to build WeatherForecast, otherwise an exception would be thrown.
        _fixture.Customize<DateOnly>(build => build.FromSeed((_) => new DateOnly(2022, 4, 4)));
    }
    
    [Theory, AutoData]
    public async void Forecast_Success(Forecast forecastDto, City city, int numberOfDays)
    {
        // This is not provided as an argument because
        var forecastsEntities = _fixture.Create<IEnumerable<WeatherForecast>>();

        // Setting-up a fake IMapper
        var mapper = A.Fake<IMapper>();
        A.CallTo(() => mapper.Map<IEnumerable<Domain.WeatherForecast>>(forecastDto.ForecastDay)).Returns(forecastsEntities);

        // Setting-up a fake IWeatherApiClient that return Forecast dto
        var weatherApiClient = A.Fake<IWeatherApiClient>();
        A.CallTo(() => weatherApiClient.GetForecast(city.Latitude, city.Longitude, numberOfDays)).Returns(forecastDto);

        var weatherForecastingService = new WeatherForecastingService(mapper, weatherApiClient);
        var result = await weatherForecastingService.Forecast(city, numberOfDays);

        result.Should().BeEquivalentTo(forecastsEntities);

        A.CallTo(() => weatherApiClient.GetForecast(
                A<decimal>.That.IsEqualTo(city.Latitude),
                A<decimal>.That.IsEqualTo(city.Longitude),
                A<int>.That.IsEqualTo(numberOfDays))
            )
            .MustHaveHappenedOnceExactly();

        A.CallTo(() => mapper.Map<IEnumerable<WeatherForecast>>(A<IEnumerable<ForecastDay>>.That.IsEqualTo(forecastDto.ForecastDay)))
            .MustHaveHappenedOnceExactly();
    }
    
    [Theory, AutoData]
    public async void Forecast_NullForecastDay_ReturnEmptyCollection(City city, int numberOfDays)
    {
        // Setting-up a fake IMapper
        var mapper = A.Fake<IMapper>();
        
        // Setting-up a fake IWeatherApiClient that return Forecast dto
        var weatherApiClient = A.Fake<IWeatherApiClient>();
        A.CallTo(() => weatherApiClient.GetForecast(city.Latitude, city.Longitude, numberOfDays)).Returns(new Forecast());

        var weatherForecastingService = new WeatherForecastingService(mapper, weatherApiClient);
        var result = await weatherForecastingService.Forecast(city, numberOfDays);

        result.Should().BeEmpty();

        A.CallTo(() => weatherApiClient.GetForecast(
                A<decimal>.That.IsEqualTo(city.Latitude),
                A<decimal>.That.IsEqualTo(city.Longitude),
                A<int>.That.IsEqualTo(numberOfDays))
            )
            .MustHaveHappenedOnceExactly();

        A.CallTo(() => mapper.Map<IEnumerable<WeatherForecast>>(A<IEnumerable<ForecastDay>>.Ignored))
            .MustNotHaveHappened();
    }
    
    [Theory, AutoData]
    public async void Forecast_ApiClientFail_PropagateException(City city, int numberOfDays)
    {
        // Setting-up a fake IMapper
        var mapper = A.Fake<IMapper>();

        // Setting-up a fake IWeatherApiClient that throws exception
        var exception = new Exception();
        var weatherApiClient = A.Fake<IWeatherApiClient>();
        A.CallTo(() => weatherApiClient.GetForecast(city.Latitude, city.Longitude, numberOfDays)).Throws(exception);

        var weatherForecastingService = new WeatherForecastingService(mapper, weatherApiClient);
        var recordedException = await Record.ExceptionAsync(() => weatherForecastingService.Forecast(city, numberOfDays));

        recordedException.Should().Be(exception);

        A.CallTo(() => weatherApiClient.GetForecast(
                A<decimal>.That.IsEqualTo(city.Latitude),
                A<decimal>.That.IsEqualTo(city.Longitude),
                A<int>.That.IsEqualTo(numberOfDays))
            )
            .MustHaveHappenedOnceExactly();

        A.CallTo(() => mapper.Map<IEnumerable<WeatherForecast>>(A<IEnumerable<ForecastDay>>.Ignored))
            .MustNotHaveHappened();
    }
}
