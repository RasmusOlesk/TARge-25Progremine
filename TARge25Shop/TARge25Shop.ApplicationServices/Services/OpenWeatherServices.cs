
using System.Text.Json;
using TARge25Shop.Core.Dto;
using TARge25Shop.Core.ServiceInterface;

namespace TARge25Shop.ApplicationServices.Services
{
    public class OpenWeatherServices : IOpenWeatherServices
    {
        private const string ApiKey = "d7d657eb82997f0ad02934db124783a2";
        private readonly HttpClient _httpClient;

        public OpenWeatherServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://api.openweathermap.org/");
        }

        public async Task<OpenWeatherRootDto> OpenWeatherResultHttpClient(
            string searchTerm)
        {
            var city = Uri.EscapeDataString(searchTerm.Trim());
            var url = $"data/2.5/weather?q={city}&units=metric&APPID={ApiKey}";

            var response = await _httpClient.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return new OpenWeatherRootDto
                {
                    Message = json
                };
            }

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var weather = JsonSerializer.Deserialize<OpenWeatherRootDto>(json, options);
            return weather ?? new OpenWeatherRootDto();
        }
    }
}

