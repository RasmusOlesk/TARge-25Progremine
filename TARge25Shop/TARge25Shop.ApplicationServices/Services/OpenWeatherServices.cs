
using System.Text.Json;
using TARge25Shop.Core.Dto;
using TARge25Shop.Core.ServiceInterface;

namespace TARge25Shop.ApplicationServices.Services
{
    public class OpenWeatherServices : IOpenWeatherServices
    {
        private readonly HttpClient _httpClient;

        public OpenWeatherServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<OpenWeatherRootDto> OpenWeatherResultHttpClient(
            string searchTerm)
        {
            var url =
                $"https://www.thecocktaildb.com/api/json/v1/1/search.php?s={Uri.EscapeDataString(searchTerm)}";

            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var weather =
                JsonSerializer.Deserialize<OpenWeatherRootDto>(json, options);

            return weather ?? new OpenWeatherRootDto();
        }
    }
}
