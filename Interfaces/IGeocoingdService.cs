using WeatherApp.Models;
using System.Threading.Tasks;

namespace WeatherApp.Interfaces;

public interface IGeocodingService
{
    Task<NominatimLocation> GetCoordinatesAsync(string placeName);
}
