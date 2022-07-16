using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Application.DI;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.DI;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.Logging;

using var host = BuildHost();

IHost BuildHost() => Host
    .CreateDefaultBuilder(args)
    .ConfigureLogging()
    .ConfigureServices((hostBuilderContext, services) =>
    {
        services.AddInfrastructure((hostBuilderContext.Configuration as IConfigurationRoot)!);
        services.AddApplicationFeatures();
    })
    .Build();

Console.WriteLine("Hello, World!");
