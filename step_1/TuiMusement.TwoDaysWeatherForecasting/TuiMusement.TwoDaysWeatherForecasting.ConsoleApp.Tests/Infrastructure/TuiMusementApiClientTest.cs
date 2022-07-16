using System.Net;
using AutoFixture.Xunit2;
using FakeItEasy;
using FluentAssertions;
using Newtonsoft.Json;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.TuiMusementApi;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.TuiMusementApi.DTO;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Tests.Helpers;
using Xunit;

namespace TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Tests.Infrastructure;

public class TuiMusementApiClientTest
{
    [Theory, AutoData]
    public async void GetAllCities_SuccessResponseStatusCode(IEnumerable<City> expectedCities)
    {
        var response = A.Fake<HttpResponseMessage>();
        response.StatusCode = HttpStatusCode.OK;
        response.Content = new StringContent(JsonConvert.SerializeObject(expectedCities));

        var httpClientFactory = IHttpClientFactoryTestHelpers.BuildFakeHttpClientFactory(TuiMusementApiClient.HttpClientName, response);

        var tuiMusementApiClient = new TuiMusementApiClient(httpClientFactory);
        var result = await tuiMusementApiClient.GetAllCities();

        // Asserting that the result should be equivalent to the expected one
        result.Should().BeEquivalentTo(expectedCities);

        // Asserting that CreateClient have been called only once
        A.CallTo(() => httpClientFactory.CreateClient(A<string>.That.IsEqualTo(TuiMusementApiClient.HttpClientName)))
            .MustHaveHappenedOnceExactly();
    }
    
    [Fact]
    public async void GetAllCities_NotFoundResponseStatusCode_ShouldReturnEmptyEnumerable()
    {
        var response = new HttpResponseMessage(HttpStatusCode.NotFound);
        var httpClientFactory = IHttpClientFactoryTestHelpers.BuildFakeHttpClientFactory(TuiMusementApiClient.HttpClientName, response);

        // Executing the test method
        var tuiMusementApiClient = new TuiMusementApiClient(httpClientFactory);
        var result = await tuiMusementApiClient.GetAllCities();

        result.Should().BeEmpty();
    }
    
    [Fact]
    public async void GetAllCities_NonSuccessResponseStatusCode_ShouldPropagateException()
    {
        var response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
        var httpClientFactory = IHttpClientFactoryTestHelpers.BuildFakeHttpClientFactory(TuiMusementApiClient.HttpClientName, response);
        
        var tuiMusementApiClient = new TuiMusementApiClient(httpClientFactory);
        var exception = await Record.ExceptionAsync(() => tuiMusementApiClient.GetAllCities());

        // Asserting that the recorded exception is of the expected type
        exception.Should().BeOfType<HttpRequestException>();
        
        // Asserting that CreateClient have been called only once
        A.CallTo(() => httpClientFactory.CreateClient(A<string>.That.IsEqualTo(TuiMusementApiClient.HttpClientName)))
            .MustHaveHappenedOnceExactly();
    }
}
