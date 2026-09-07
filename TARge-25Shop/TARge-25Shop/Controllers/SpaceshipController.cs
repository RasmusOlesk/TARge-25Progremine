using Microsoft.AspNetCore.Mvc;

namespace TARge_25Shop.Controllers
{
    public class SpaceshipController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}
