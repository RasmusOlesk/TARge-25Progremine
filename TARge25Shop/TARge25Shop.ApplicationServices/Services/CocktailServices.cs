
using System.Text.Json;
using TARge25Shop.Core.Dto;
using TARge25Shop.Core.ServiceInterface;

namespace TARge25Shop.ApplicationServices.Services
{
    public class CocktailServices : ICocktailServices
    {
        private readonly HttpClient _httpClient;

        public CocktailServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CocktailRootDto> CocktailResultHttpClient(
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

            var drink =
                JsonSerializer.Deserialize<CocktailRootDto>(json, options);

            return drink ?? new CocktailRootDto();
        }
    }
}
