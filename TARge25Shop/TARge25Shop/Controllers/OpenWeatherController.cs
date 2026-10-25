
using Microsoft.AspNetCore.Mvc;
using TARge25Shop.Core.ServiceInterface;
using TARge25Shop.Models.OpenWeather;

namespace TARge25Shop.Controllers
{
    public class OpenWeatherController : Controller
    {
        private readonly IOpenWeatherServices _openWeatherServices;

        public OpenWeatherController(IOpenWeatherServices openWeatherServices)
        {
            _openWeatherServices = openWeatherServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Weather(string? searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return View("Weather");
            }

            var result = await _openWeatherServices.OpenWeatherResultHttpClient(searchTerm);

            if (string.IsNullOrWhiteSpace(result.Name) || result.Main == null)
            {
                ViewBag.Message = !string.IsNullOrWhiteSpace(result.Message)
                    ? result.Message
                    : $"No weather found for '{searchTerm}'.";
                return View("Weather");
            }

            var weather = result.Weather?.FirstOrDefault();

            var vm = new OpenWeatherViewModel
            {
                CityName = result.Name,
                Country = result.Sys?.Country ?? string.Empty,
                Temperature = result.Main.Temp ?? 0,
                FeelsLike = result.Main.FeelsLike ?? 0,
                TempMin = result.Main.TempMin ?? 0,
                TempMax = result.Main.TempMax ?? 0,
                Pressure = result.Main.Pressure ?? 0,
                Humidity = result.Main.Humidity ?? 0,
                WindSpeed = result.Wind?.Speed ?? 0,
                WindDegrees = result.Wind?.Deg ?? 0,
                Cloudiness = result.Clouds?.All ?? 0,
                Visibility = result.Visibility ?? 0,
                Description = weather?.Description ?? "No description available",
                Icon = weather?.Icon ?? string.Empty,
                Sunrise = result.Sys?.Sunrise is long sunrise
                    ? DateTimeOffset.FromUnixTimeSeconds(sunrise).LocalDateTime
                    : DateTime.MinValue,
                Sunset = result.Sys?.Sunset is long sunset
                    ? DateTimeOffset.FromUnixTimeSeconds(sunset).LocalDateTime
                    : DateTime.MinValue
            };

            return View("Weather", vm);
        }
    }
}

