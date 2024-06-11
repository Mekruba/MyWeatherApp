using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WeatherApp.Models;

namespace WeatherApp.Pages.WeatherForecast
{
    public class WeatherForecastModel : PageModel
    {
        private readonly IHttpClientFactory _clientfactory;
        public Forecast.Root ForecastData { get; set; }
        public WeatherForecastModel(IHttpClientFactory clientFactory)
        {
            _clientfactory = clientFactory;
        }
        public async Task OnGetAsync()
        {
            string latitude = "60.10";
            string longitude = "9.58";
            string url = $"https://api.met.no/weatherapi/locationforecast/2.0/compact?lat={latitude}&lon={longitude}";

            var client = _clientfactory.CreateClient();
            client.DefaultRequestHeaders.Add("User-Agent", "MyWeatherApp");

            HttpResponseMessage response = await client.GetAsync(url);

            string content = await response.Content.ReadAsStringAsync();

            ForecastData = JsonConvert.DeserializeObject<Forecast.Root>(content);

            Console.WriteLine("Done");
        }
    }
}
