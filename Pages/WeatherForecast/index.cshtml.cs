using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WeatherApp.Interfaces;
using WeatherApp.Models;

namespace WeatherApp.Pages.WeatherForecast
{
    public class WeatherForecastModel : PageModel
    {
        private readonly IWeatherForcastService _weatherForecastSerivce;

        public Forecast.Root ForecastData { get; set; }
        public WeatherForecastModel(IWeatherForcastService weatherForecastService)
        {
            _weatherForecastSerivce = weatherForecastService;
        }

        [BindProperty(SupportsGet = true)]
        public double Latitude {get;set;}

        [BindProperty(SupportsGet = true)]
        public double Longitude {get;set;}

        public async Task<IActionResult> OnGetAsync()
        {
            if (Latitude == 0 || Longitude == 0)
            {
                return NotFound("Coordinates not provided");
            }
            var forecast = await _weatherForecastSerivce.GetWetherAsync(Latitude, Longitude);
            ForecastData = forecast;

            
            return Page();
        }
    }
}
