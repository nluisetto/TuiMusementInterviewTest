using AutoMapper;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.CityRepository;
using Xunit;

namespace TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Tests.Infrastructure.CityRepository;

public class MappingProfileTest
{
    [Fact]
    public void MappingProfile_ValidConfiguration()
    {
        var mapperConfiguration = new MapperConfiguration((option) => option.AddProfile(typeof(MappingProfile)));
        mapperConfiguration.AssertConfigurationIsValid();
    }
}
