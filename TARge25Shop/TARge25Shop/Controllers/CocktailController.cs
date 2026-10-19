using Microsoft.AspNetCore.Mvc;
using TARge25Shop.Core.ServiceInterface;
using TARge25Shop.Models.Cocktail;

namespace TARge25Shop.Controllers
{
    public class CocktailController : Controller
    {
        private readonly ICocktailServices _cocktailServices;

        public CocktailController
            (
                ICocktailServices cocktailServices
            )
        {
            _cocktailServices = cocktailServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Drink()
        {
            var drink = await _cocktailServices.CocktailResultHttpClient();

            CocktailViewModel vm = new();

            //vm.Categories = drink.Categories;
            vm.strDrink = drink.strDrink;
            vm.strDrinkAlternate = drink.strDrinkAlternate;
            vm.strTags = drink.strTags;
            vm.strVideo = drink.strVideo;
            vm.strCategory = drink.strCategory;
            vm.strIBA = drink.strIBA;
            vm.strDrink = drink.strDrink;
            vm.strAlcoholic = drink.strAlcoholic;
            vm.strGlass = drink.strGlass;
            vm.strInstructions = drink.strInstructions;
            vm.strInstructionsES = drink.strInstructionsES;
            vm.strInstructionsDE = drink.strInstructionsDE;
            vm.strInstructionsFR = drink.strInstructionsFR;
            vm.strInstructionsIT = drink.strInstructionsIT;
            vm.strDrinkThumb = drink.strDrinkThumb;
            vm.strIngredient1 = drink.strIngredient1;
            vm.strIngredient2 = drink.strIngredient2;
            vm.strIngredient3 = drink.strIngredient3;
            vm.strIngredient4 = drink.strIngredient4;
            vm.strIngredient5 = drink.strIngredient5;
            vm.strIngredient6 = drink.strIngredient6;
            vm.strIngredient7 = drink.strIngredient7;
            vm.strIngredient8 = drink.strIngredient8;
            vm.strIngredient9 = drink.strIngredient9;
            vm.strIngredient10 = drink.strIngredient10;
            vm.strIngredient11 = drink.strIngredient11;
            vm.strIngredient12 = drink.strIngredient12;
            vm.strIngredient13 = drink.strIngredient13;
            vm.strIngredient14 = drink.strIngredient14;
            vm.strIngredient15 = drink.strIngredient15;
            vm.strMeasure1 = drink.strMeasure1;
            vm.strMeasure2 = drink.strMeasure2;
            vm.strMeasure3 = drink.strMeasure3;
            vm.strMeasure4 = drink.strMeasure4;
            vm.strMeasure5 = drink.strMeasure5;
            vm.strMeasure6 = drink.strMeasure6;
            vm.strMeasure7 = drink.strMeasure7;
            vm.strMeasure8 = drink.strMeasure8;
            vm.strMeasure9 = drink.strMeasure9;
            vm.strMeasure10 = drink.strMeasure10;
            vm.strMeasure11 = drink.strMeasure11;
            vm.strMeasure12 = drink.strMeasure12;
            vm.strMeasure13 = drink.strMeasure13;
            vm.strMeasure14 = drink.strMeasure14;
            vm.strMeasure15 = drink.strMeasure15;
            vm.strImageSource = drink.strImageSource;
            vm.strImageAttribution = drink.strImageAttribution;
            vm.strCreativeCommonsConfirmed = drink.strCreativeCommonsConfirmed;
            vm.dateModified = drink.dateModified;







            return View("Drink", vm);
        }
    }
}