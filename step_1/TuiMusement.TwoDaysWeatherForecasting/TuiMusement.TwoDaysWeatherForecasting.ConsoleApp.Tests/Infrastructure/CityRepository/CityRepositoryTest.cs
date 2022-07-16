using Xunit;
using AutoFixture.Xunit2;
using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.TuiMusementApi;
using CityDto = TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.TuiMusementApi.DTO.City;
using CityEntity = TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Domain.City;

namespace TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Tests.Infrastructure.CityRepository;

public class CityRepositoryTest
{
    [Theory, AutoData]
    public async void All_Success(IEnumerable<CityDto> cityDtos, IEnumerable<CityEntity> cityEntities)
    {
        // Setting-up a fake IMapper
        var mapper = A.Fake<IMapper>();
        A.CallTo(() => mapper.Map<IEnumerable<CityEntity>>(cityDtos)).Returns(cityEntities);

        // Setting-up a fake ITuiMusementApiClient that return two Dummy City DTOs
        var tuiMusementApiClient = A.Fake<ITuiMusementApiClient>();
        A.CallTo(() => tuiMusementApiClient.GetAllCities()).Returns(cityDtos);
        
        // Executing the test method
        var cityRepository = new ConsoleApp.Infrastructure.CityRepository.CityRepository(mapper, tuiMusementApiClient);
        var result = await cityRepository.All();
        
        // Asserting that output from CityRepository.All match the size of the dummy City DTO collection returned by the fake ITuiMusementApiClient 
        result.Should().BeEquivalentTo(cityEntities);
        
        // Asserting that tuiMusementApiClient.GetAll have been called once
        A.CallTo(() => tuiMusementApiClient.GetAllCities()).MustHaveHappenedOnceExactly();

        // Asserting that mapper.Map have been called once
        A.CallTo(() => mapper.Map<IEnumerable<CityEntity>>(cityDtos)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async void All_ApiClientFail_PropagateException()
    {
        // Setting-up a fake IMapper
        var mapper = A.Fake<IMapper>();
        
        // Setting-up a fake ITuiMusementApiClient that throw exception
        var exception = A.Dummy<Exception>();
        var tuiMusementApiClient = A.Fake<ITuiMusementApiClient>();
        A.CallTo(() => tuiMusementApiClient.GetAllCities()).Throws(exception);
        
        // Executing the test method
        var cityRepository = new ConsoleApp.Infrastructure.CityRepository.CityRepository(mapper, tuiMusementApiClient);
        var thrownException = await Record.ExceptionAsync(async () => await cityRepository.All());
        
        // Asserting the captured Exception is the same thrown by TuiMusementApiClient
        Assert.Same(exception, thrownException);
        
        // Asserting that tuiMusementApiClient.GetAll have been called once
        A.CallTo(() => tuiMusementApiClient.GetAllCities()).MustHaveHappenedOnceExactly();
        
        // Asserting that mapper.Map have not been called
        A.CallTo(() => mapper.Map<IEnumerable<CityEntity>>(A<IEnumerable<CityDto>>.Ignored)).MustNotHaveHappened();
    }
}
