using System.Net.Http.Headers;

namespace TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.HttpClient;

public static class HttpRequestHeadersExtensions
{
    private const string AcceptHeaderName = "Accept";
    private const string AcceptHeaderApplicationJsonValue = "application/json";

    public static void AddAcceptApplicationJson(this HttpRequestHeaders headers)
    {
        headers.Add(AcceptHeaderName, AcceptHeaderApplicationJsonValue);
    }
}
