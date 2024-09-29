using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using WeatherApp.Interfaces;
using WeatherApp.Models;

namespace WeatherApp.Services;

public class WeatherForecastService : IWeatherForcastService
{

    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _cache;

    public WeatherForecastService(HttpClient httoClient, IMemoryCache cache)
    {
        _httpClient = httoClient;
        _cache = cache;

        _httpClient.DefaultRequestHeaders.Add("User-Agent", "MyWeatherApp");
    }
    public async Task<Forecast.Root> GetWetherAsync(double latitude, double longitude)
    {
        string latitudeString = latitude.ToString("F4");
        string longitudeStrinng = longitude.ToString("F4");

        string cacheKey = $"{latitudeString},{longitudeStrinng}";

        if (_cache.TryGetValue(cacheKey, out Forecast.Root cachedForecast))
        {
            return cachedForecast;
        }

        string requestUri = $"https://api.met.no/weatherapi/locationforecast/2.0/compact?lat={latitudeString}&lon={longitudeStrinng}";

        var response = await _httpClient.GetAsync(requestUri);

        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            Forecast.Root ForecastData = JsonConvert.DeserializeObject<Forecast.Root>(jsonString);

            if (ForecastData != null)
            {
                _cache.Set(cacheKey, ForecastData, TimeSpan.FromMinutes(10));

                return ForecastData;
            }

        }
        throw new Exception("Unable to get weather data for the specified location.");
    }
}