using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Application.DI;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Application.Features.TwoDaysWeatherForecasting;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.DI;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.Logging;

using var host = BuildHost();

using IServiceScope scope = host.Services.CreateAsyncScope();

var twoDaysWeatherForecastingService = scope.ServiceProvider.GetRequiredService<ITwoDaysWeatherForecastingService>();
await twoDaysWeatherForecastingService.Execute();



IHost BuildHost() => Host
    .CreateDefaultBuilder(args)
    .ConfigureLogging()
    .ConfigureServices((hostBuilderContext, services) =>
    {
        services.AddInfrastructure((hostBuilderContext.Configuration as IConfigurationRoot)!);
        services.AddApplicationFeatures();
    })
    .Build();
