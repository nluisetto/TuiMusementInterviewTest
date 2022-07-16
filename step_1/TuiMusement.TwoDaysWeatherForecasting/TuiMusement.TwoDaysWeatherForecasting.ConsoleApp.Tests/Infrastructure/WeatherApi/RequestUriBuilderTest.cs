using AutoFixture.Xunit2;
using FluentAssertions;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.WeatherApi;
using Xunit;

namespace TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Tests.Infrastructure.WeatherApi;

public class RequestUriBuilderTest
{
    [Theory, AutoData]
    public void BuildForecastingUri_ReturnProperValue(string apiKey, decimal latitude, decimal longitude, int numberOfDays)
    {
        var expectedResult = $"{Paths.Forecast}?key={apiKey}&q={latitude},{longitude}&days={numberOfDays}";
        
        var requestUriBuilder = new RequestUriBuilder();
        var result = requestUriBuilder.BuildForecastingUri(apiKey, latitude, longitude, numberOfDays);

        result.Should().BeEquivalentTo(expectedResult);
    }
}
