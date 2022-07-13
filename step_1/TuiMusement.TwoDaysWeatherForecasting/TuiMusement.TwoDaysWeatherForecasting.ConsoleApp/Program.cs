using Microsoft.Extensions.Hosting;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.Logging;

using var host = BuildHost();

IHost BuildHost() => Host
    .CreateDefaultBuilder(args)
    .ConfigureLogging()
    .Build();

Console.WriteLine("Hello, World!");
