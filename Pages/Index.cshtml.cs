using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeatherApp.Interfaces;

namespace WeatherApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IGeocodingService _geocodingService;

    [BindProperty]
    public string Location {get;set;}


    public IndexModel(ILogger<IndexModel> logger, IGeocodingService geocodingService)
    {
        _logger = logger;
        _geocodingService = geocodingService;
    }

    public void OnGet()
    {

    }

    public async Task<IActionResult> OnPostAsync()
    {
        if(string.IsNullOrEmpty(Location))
        {
             // If no location is provided, stay on the page and show an error (optional)
            ModelState.AddModelError(string.Empty, "Location is required.");
            return Page();
        }

        var locationData = await _geocodingService.GetCoordinatesAsync(Location);
        double latitude = double.Parse(locationData.Lat);
        double longitude = double.Parse(locationData.Lon);

        return RedirectToPage("WeatherForecast/index", new{Latitude = latitude, Longitude = longitude});
    }
}
