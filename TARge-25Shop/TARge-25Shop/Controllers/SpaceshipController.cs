using Microsoft.AspNetCore.Mvc;
using TARge25SHop.Core.Dto;
using TARge_25Shop.Models.Spaceship;
using TARge25Shop.ApplicationServices.Services;
using TARge25SHop.Core.ServiceInterface;

namespace TARge_25Shop.Controllers
{
    public class SpaceshipController : Controller
    {

        private readonly ISpaceshipServices _spaceshipServices;

        public SpaceshipController
            (
                ISpaceshipServices spaceshipServices
            )
        {
            _spaceshipServices = spaceshipServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SpaceshipCreateViewModel vm)
        {
            var dto = new SpaceshipDto
            {
                Name = vm.Name,
                ShipType = vm.ShipType,
                Crew = vm.Crew,
                EnginePower = vm.EnginePower
            };

            //Nüüd kutsume teenuse välja, et luua uus kosmoselaev. See on
            //asünkroone tegevus ja kasutame await.
            var result = await _spaceshipServices.Create(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));

            }

            return RedirectToAction(nameof(Index));
        }
    }
}
