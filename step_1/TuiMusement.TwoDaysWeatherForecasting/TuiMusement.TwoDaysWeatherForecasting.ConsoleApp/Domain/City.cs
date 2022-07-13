namespace TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Domain;

public class City
{
    public string Name { get; }
    public decimal Latitude { get; }
    public decimal Longitude { get; }

    public City(string name, decimal latitude, decimal longitude)
    {
        Name = name;
        Latitude = latitude;
        Longitude = longitude;
    }
}
