
using Microsoft.AspNetCore.Mvc;
using TARge25Shop.Core.ServiceInterface;
using TARge25Shop.Models.Cocktail;

namespace TARge25Shop.Controllers
{
    public class CocktailController : Controller
    {
        private readonly ICocktailServices _cocktailServices;

        public CocktailController(ICocktailServices cocktailServices)
        {
            _cocktailServices = cocktailServices;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Drink(string? searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return View("Drink");
            }

            var result = await _cocktailServices
                .CocktailResultHttpClient(searchTerm);

            var cocktail = result.Drinks?.FirstOrDefault();

            if (cocktail == null)
            {
                ViewBag.Message = $"No drinks found for '{searchTerm}'.";
                return View("Drink");
            }

            var vm = new CocktailViewModel
            {
                strDrink = cocktail.StrDrink,
                strDrinkAlternate = cocktail.StrDrinkAlternate,
                strTags = cocktail.StrTags,
                strVideo = cocktail.StrVideo,
                strCategory = cocktail.StrCategory,
                strIBA = cocktail.StrIBA,
                strAlcoholic = cocktail.StrAlcoholic,
                strGlass = cocktail.StrGlass,
                strInstructions = cocktail.StrInstructions,
                strInstructionsES = cocktail.StrInstructionsES,
                strInstructionsDE = cocktail.StrInstructionsDE,
                strInstructionsFR = cocktail.StrInstructionsFR,
                strInstructionsIT = cocktail.StrInstructionsIT,
                strDrinkThumb = cocktail.StrDrinkThumb,

                strIngredient1 = cocktail.StrIngredient1,
                strIngredient2 = cocktail.StrIngredient2,
                strIngredient3 = cocktail.StrIngredient3,
                strIngredient4 = cocktail.StrIngredient4,
                strIngredient5 = cocktail.StrIngredient5,
                strIngredient6 = cocktail.StrIngredient6,
                strIngredient7 = cocktail.StrIngredient7,
                strIngredient8 = cocktail.StrIngredient8,
                strIngredient9 = cocktail.StrIngredient9,
                strIngredient10 = cocktail.StrIngredient10,
                strIngredient11 = cocktail.StrIngredient11,
                strIngredient12 = cocktail.StrIngredient12,
                strIngredient13 = cocktail.StrIngredient13,
                strIngredient14 = cocktail.StrIngredient14,
                strIngredient15 = cocktail.StrIngredient15,

                strMeasure1 = cocktail.StrMeasure1,
                strMeasure2 = cocktail.StrMeasure2,
                strMeasure3 = cocktail.StrMeasure3,
                strMeasure4 = cocktail.StrMeasure4,
                strMeasure5 = cocktail.StrMeasure5,
                strMeasure6 = cocktail.StrMeasure6,
                strMeasure7 = cocktail.StrMeasure7,
                strMeasure8 = cocktail.StrMeasure8,
                strMeasure9 = cocktail.StrMeasure9,
                strMeasure10 = cocktail.StrMeasure10,
                strMeasure11 = cocktail.StrMeasure11,
                strMeasure12 = cocktail.StrMeasure12,
                strMeasure13 = cocktail.StrMeasure13,
                strMeasure14 = cocktail.StrMeasure14,
                strMeasure15 = cocktail.StrMeasure15,

                strImageSource = cocktail.StrImageSource,
                strImageAttribution = cocktail.StrImageAttribution,
                strCreativeCommonsConfirmed =
                    cocktail.StrCreativeCommonsConfirmed,
                dateModified = cocktail.DateModified
            };

            return View("Drink", vm);
        }

    }
}
