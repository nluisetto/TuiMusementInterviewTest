using System.Collections.Immutable;
using System.Net;
using System.Net.Http.Json;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.TuiMusementApi.DTO;

namespace TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.TuiMusementApi;

public class TuiMusementApiClient : ITuiMusementApiClient
{
    public const string HttpClientName = "TuiMusement";

    private readonly IHttpClientFactory _httpClientFactory;
    
    
    
    public TuiMusementApiClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    
    
    public async Task<IEnumerable<City>> GetAllCities()
    {
        using var httpClient = _httpClientFactory.CreateClient(HttpClientName);
        
        var response = await httpClient.GetAsync(Paths.CitiesCollection);
        
        if (response.StatusCode == HttpStatusCode.NotFound)
            return ImmutableList<City>.Empty;
        
        response.EnsureSuccessStatusCode();
        
        return (await response.Content.ReadFromJsonAsync<IEnumerable<City>>())!;
    }
}
