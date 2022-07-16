using System.Net;
using AutoFixture.Xunit2;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.WeatherApi;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.WeatherApi.Configuration;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.WeatherApi.DTO;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Tests.Helpers;
using Xunit;

namespace TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Tests.Infrastructure.WeatherApi;

public class WeatherApiClientTest
{
    [Theory, AutoData]
    public async void GetAllCities_SuccessResponseStatusCode(WeatherApiConfiguration weatherApiConfiguration, decimal latitude, decimal longitude, int numberOfDays, Forecast expectedForecast)
    {
        var weatherApiConfigurationOptionsSnapshot = A.Fake<IOptionsSnapshot<WeatherApiConfiguration>>();
        A.CallTo(() => weatherApiConfigurationOptionsSnapshot.Value).Returns(weatherApiConfiguration);

        var response = A.Fake<HttpResponseMessage>();
        response.StatusCode = HttpStatusCode.OK;
        response.Content = new StringContent(JsonConvert.SerializeObject(new ForecastResponse { Forecast = expectedForecast }));

        var httpClientFactory = IHttpClientFactoryTestHelpers.BuildFakeHttpClientFactory(WeatherApiClient.HttpClientName, response);

        var requestUriBuilder = A.Fake<IRequestUriBuilder>();
        A.CallTo(() => requestUriBuilder.BuildForecastingUri(
                A<string>.Ignored,
                A<decimal>.Ignored,
                A<decimal>.Ignored,
                A<int>.Ignored)
            )
            .Returns("/a/fake/request/uri");

        var weatherApiClient = new WeatherApiClient(weatherApiConfigurationOptionsSnapshot, httpClientFactory, requestUriBuilder);
        var result = await weatherApiClient.GetForecast(latitude, longitude, numberOfDays);

        // Asserting that the result should be equivalent to the expected one
        result.Should().BeEquivalentTo(expectedForecast);

        // Asserting that CreateClient have been called only once
        A.CallTo(() => httpClientFactory.CreateClient(A<string>.That.IsEqualTo(WeatherApiClient.HttpClientName)))
            .MustHaveHappenedOnceExactly();

        A.CallTo(() => requestUriBuilder.BuildForecastingUri(
                A<string>.That.IsEqualTo(weatherApiConfiguration.ApiKey!),
                A<decimal>.That.IsEqualTo(latitude),
                A<decimal>.That.IsEqualTo(longitude),
                A<int>.That.IsEqualTo(numberOfDays))
            )
            .MustHaveHappenedOnceExactly();
    }
    
    [Theory, AutoData]
    public async void GetAllCities_NonSuccessResponseStatusCode_ShouldPropagateException(WeatherApiConfiguration weatherApiConfiguration, decimal latitude, decimal longitude, int numberOfDays, Forecast expectedForecast)
    {
        var weatherApiConfigurationOptionsSnapshot = A.Fake<IOptionsSnapshot<WeatherApiConfiguration>>();
        A.CallTo(() => weatherApiConfigurationOptionsSnapshot.Value).Returns(weatherApiConfiguration);
        
        var response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
        var httpClientFactory = IHttpClientFactoryTestHelpers.BuildFakeHttpClientFactory(WeatherApiClient.HttpClientName, response);

        var requestUriBuilder = A.Fake<IRequestUriBuilder>();
        A.CallTo(() => requestUriBuilder.BuildForecastingUri(
                A<string>.Ignored,
                A<decimal>.Ignored,
                A<decimal>.Ignored,
                A<int>.Ignored)
            )
            .Returns("/a/fake/request/uri");
        
        var weatherApiClient = new WeatherApiClient(weatherApiConfigurationOptionsSnapshot, httpClientFactory, requestUriBuilder);
        var exception = await Record.ExceptionAsync(() => weatherApiClient.GetForecast(latitude, longitude, numberOfDays));

        // Asserting that the recorded exception is of the expected type
        exception.Should().BeOfType<HttpRequestException>();
        
        // Asserting that CreateClient have been called only once
        A.CallTo(() => httpClientFactory.CreateClient(A<string>.That.IsEqualTo(WeatherApiClient.HttpClientName)))
            .MustHaveHappenedOnceExactly();
        
        A.CallTo(() => requestUriBuilder.BuildForecastingUri(
                A<string>.That.IsEqualTo(weatherApiConfiguration.ApiKey!),
                A<decimal>.That.IsEqualTo(latitude),
                A<decimal>.That.IsEqualTo(longitude),
                A<int>.That.IsEqualTo(numberOfDays))
            )
            .MustHaveHappenedOnceExactly();
    }
}
