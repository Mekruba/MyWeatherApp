using WeatherApp.Models;

namespace WeatherApp.Interfaces;

public interface IWeatherForcastService
{
    Task<Forecast.Root> GetWetherAsync(double latitude, double longitude);
}