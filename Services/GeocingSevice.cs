using Newtonsoft.Json;
using WeatherApp.Interfaces;
using WeatherApp.Models;

namespace WeatherApp.Services;

public class GeocodingService : IGeocodingService
{
    private readonly HttpClient _httpClient;

    public GeocodingService(HttpClient httpClient)
    {
        _httpClient = httpClient;

      //  _httpClient.BaseAddress = new Uri("https://nominatim.openstreetmap.org/");

        _httpClient.DefaultRequestHeaders.Add("User-Agent", "MyWeatherApp");
    }
    public async Task<NominatimLocation> GetCoordinatesAsync(string placeName)
    {
        string requestUri = $"https://nominatim.openstreetmap.org/search?q={placeName}&format=jsonv2";
        var response = await _httpClient.GetAsync(requestUri);

        if(response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var locations = JsonConvert.DeserializeObject<List<NominatimLocation>>(jsonString);

            if(locations != null && locations.Count> 0)
            {
                return locations[0];
            }
        }
        throw new Exception("Unable to get coordinates for the specified location.");
    }
}