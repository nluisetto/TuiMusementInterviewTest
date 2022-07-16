using FakeItEasy;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.TuiMusementApi;

namespace TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Tests.Helpers;

public static class IHttpClientFactoryTestHelpers
{
    public static IHttpClientFactory BuildFakeHttpClientFactory(string httpClientName, HttpResponseMessage response)
    {
        // Setting-up fake HttpMessageHandler that will return the HttpResponseMessage given as argument
        var httpMessageHandler = A.Fake<HttpMessageHandler>();
        A.CallTo(httpMessageHandler).Where(call => call.Method.Name.Equals("SendAsync")).WithReturnType<Task<HttpResponseMessage>>().Returns(response);

        // Create HttpClient that will use the HttpMessageHandler created before
        var httpClient = new HttpClient(httpMessageHandler);
        httpClient.BaseAddress = new Uri("https://a.fake.url");

        // Setting-up fake IHttpClientFactory that will return the previously created HttpClient
        var httpClientFactory = A.Fake<IHttpClientFactory>();
        A.CallTo(() => httpClientFactory.CreateClient(A<string>.That.IsEqualTo(httpClientName))).Returns(httpClient);

        return httpClientFactory;
    }
}
