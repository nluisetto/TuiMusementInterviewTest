using AutoMapper;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.WeatherForecasting;
using Xunit;

namespace TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Tests.Infrastructure.WeatherForecasting;

public class MappingProfileTest
{
    [Fact]
    public void MappingProfile_ValidConfiguration()
    {
        var mapperConfiguration = new MapperConfiguration((option) => option.AddProfile(typeof(MappingProfile)));
        mapperConfiguration.AssertConfigurationIsValid();
    }
}
