using AutoMapper;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Domain;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.TuiMusementApi;
using CityDto = TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.TuiMusementApi.DTO.City;
using CityEntity = TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Domain.City;

namespace TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.CityRepository;

public class CityRepository : ICityRepository
{
    private readonly IMapper _mapper;
    private readonly ITuiMusementApiClient _tuiMusementApiClient;
    
    public CityRepository(IMapper mapper, ITuiMusementApiClient tuiMusementApiClient)
    {
        _mapper = mapper;
        _tuiMusementApiClient = tuiMusementApiClient;
    }
    
    
    
    public async Task<IEnumerable<City>> All()
    {
        var cityDtoCollection = await _tuiMusementApiClient.GetAllCities();

        return _mapper.Map<IEnumerable<CityEntity>>(cityDtoCollection);
    }
}
