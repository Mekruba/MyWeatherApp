using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class WeatherForecastModel : PageModel
    {
        private readonly IHttpClientFactory _clientfactory;
        public WeatherForecastModel(IHttpClientFactory clientFactory)
        {
            _clientfactory = clientFactory;
        }
        public async Task OnAsyncGet()
        {
            string latitude = "60.10";
            string longitude = "9.58";
            string url = $"https://api.met.no/weatherapi/locationforecast/2.0/compact?lat={latitude}&lon={longitude}";

            var client = _clientfactory.CreateClient();
            client.DefaultRequestHeaders.Add("User-Agent", "YourApplicationName");
        }
    }
}
