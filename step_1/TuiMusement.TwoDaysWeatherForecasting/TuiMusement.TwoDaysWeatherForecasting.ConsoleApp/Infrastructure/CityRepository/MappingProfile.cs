using AutoMapper;

namespace TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.CityRepository;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TuiMusementApi.DTO.City, Domain.City>();
    }
}
