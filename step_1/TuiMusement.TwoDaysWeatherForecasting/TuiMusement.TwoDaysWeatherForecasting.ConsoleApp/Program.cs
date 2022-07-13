using Microsoft.Extensions.Hosting;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.DI;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.Logging;

using var host = BuildHost();

IHost BuildHost() => Host
    .CreateDefaultBuilder(args)
    .ConfigureLogging()
    .ConfigureServices((_, services) =>
    {
        services.AddInfrastructure();
    })
    .Build();

Console.WriteLine("Hello, World!");
