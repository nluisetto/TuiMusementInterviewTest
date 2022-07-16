using AutoMapper;

namespace TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.WeatherForecasting;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<WeatherApi.DTO.ForecastDay, Domain.WeatherForecast>()
            .ForCtorParam("date", rule => rule.MapFrom(source => DateOnly.FromDateTime(source.Date)))
            .ForCtorParam("weatherCondition", rule => rule.MapFrom(source => source.Day.Condition.Text));
    }
}
