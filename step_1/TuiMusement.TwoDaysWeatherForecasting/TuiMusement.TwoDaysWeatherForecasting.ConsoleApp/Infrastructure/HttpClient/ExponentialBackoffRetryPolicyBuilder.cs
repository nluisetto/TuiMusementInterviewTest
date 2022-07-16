using Polly;
using Polly.Extensions.Http;

namespace TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.HttpClient;

public static class ExponentialBackoffRetryPolicyBuilder
{
    public static IAsyncPolicy<HttpResponseMessage> GetPolicy(int retryCount)
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }
}
