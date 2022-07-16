using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.TypeMapping.DI;

public static class TypeMappingServiceCollectionExtensions
{
    public static IServiceCollection AddTypeMapping(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddAutoMapper(options => options.AddMaps(Assembly.GetExecutingAssembly()));
    }
}
