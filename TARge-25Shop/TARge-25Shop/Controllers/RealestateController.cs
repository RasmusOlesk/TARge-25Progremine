using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TARge25Shop.Core.Dto;
using TARge25Shop.Core.ServiceInterface;
using TARge25Shop.Data;
using TARge25Shop.Models.Realestate;
using TARge25Shop.Models.Spaceship;
using TARge25SHop.Core.Dto;

namespace TARge25Shop.Controllers
{
    public class RealEstateController : Controller
    {
        private readonly IRealEstateServices _RealEstateServices;
        private readonly TARge25ShopContext _context;
        private readonly IFileServices _fileServices;


        public RealEstateController
            (
                IRealEstateServices realestateServices,
                TARge25ShopContext context,
                IFileServices fileServices
            )
        {
            _RealEstateServices = realestateServices;
            _context = context;
            _fileServices = fileServices;
        }

        public IActionResult Index()
        {

            // Kutsume teenuse välja, et saada kõik kosmoselaevad. 
            //constructoris tuleb välja kutsuda DbContext, et
            //saaksime andmeid kätte.
            var result = _context.RealEstates
                .Select(x => new RealEstateIndexViewModel
                {
                    Id = x.Id,
                    Area = x.Area,
                    Location = x.Location,
                    RoomNumber = x.RoomNumber,
                    BuildingType = x.BuildingType,
                    CreatedAt = (DateTime)x.CreatedAt,
                });

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            RealestateCreateModifyViewModel result = new();

            return View("CreateModify", result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RealestateCreateModifyViewModel vm)
        {
            var dto = new RealEstateDto
            {
                Area = vm.Area,
                Location = vm.Location,
                RoomNumber = vm.RoomNumber,
                BuildingType = vm.BuildingType,
                //failide lisamine
                Files = vm.Files,
                Image = vm.Image
                    .Select(x => new FileToDatabaseDto
                    {
                        Id = x.ImageId,
                        ImageData = x.ImageData,
                        ImageTitle = x.ImageTitle,
                        RealEstateId = x.RealEstateId
                    }).ToArray()
            };

            //Nüüd kutsume teenuse välja, et luua uus kosmoselaev. See on
            //asünkroonne tegevus ja kasutame await.
            var result = await _RealEstateServices.Create(dto);

            if (result == null)
            {
                // Kui kosmoselaeva loomine ebaõnnestus, siis võime kuvada veateate
                // ja jätta kasutaja samale lehele.
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Modify(Guid id)
        {
            var realestate = await _RealEstateServices.DetailAsync(id);

            if (realestate == null)
            {
                return NotFound();
            }

            var images = await _context.FilesToDatabases
                .Where(x => x.RealEstateId == id)
                .Select(y => new RealestateImageViewModel
                {
                    Image = y.ImageTitle,
                    ImageId = y.Id
                }).ToArrayAsync();


            var vm = new RealestateCreateModifyViewModel();

            vm.Id = realestate.Id;
            vm.Area = realestate.Area;
            vm.Location = realestate.Location;
            vm.RoomNumber = realestate.RoomNumber;
            vm.BuildingType = realestate.BuildingType;
            vm.CreatedAt = (DateTime)realestate.CreatedAt;
            vm.ModifiedAt = (DateTime)realestate.ModifiedAt;
            vm.Image.AddRange(images);

            return View("CreateModify", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Modify(RealestateCreateModifyViewModel vm)
        {
            var dto = new RealEstateDto()
            {
                Id = vm.Id,
                Area = vm.Area,
                Location = vm.Location,
                RoomNumber = vm.RoomNumber,
                BuildingType = vm.BuildingType,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,
                Files = vm.Files,
                Image = vm.Image
                    .Select(x => new FileToDatabaseDto
                    {
                        Id = x.ImageId,
                        ImageTitle = x.ImageTitle,
                        RealEstateId = x.RealEstateId
                    }).ToArray()
            };

            var result = await _RealEstateServices.Modify(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var realestate = await _RealEstateServices.DetailAsync(id);

            if (realestate == null)
            {
                return NotFound();
            }

            var images = await _context.FilesToDatabases
                .Where(x => x.RealestateId == id)
                .Select(y => new RealestateImageViewModel
                {
                    Image = y.ImageTitle,
                    ImageId = y.Id
                }).ToArrayAsync();

            //see on vaheinstants domaini ja vm vahel
            var vm = new RealEstateDeleteViewModel();

            vm.Id = realestate.Id;
            vm.Area = realestate.Area;
            vm.Location = realestate.Location;
            vm.RoomNumber = realestate.RoomNumber;
            vm.BuildingType = realestate.BuildingType;
            vm.CreatedAt = (DateTime)realestate.CreatedAt;
            vm.ModifiedAt = (DateTime)realestate.ModifiedAt;
            

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var realestate = await _RealEstateServices.Delete(id);

            if (realestate == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        //teha Detaili vaate meetod
        public async Task<IActionResult> Details(Guid id)
        {
            var realestate = await _RealEstateServices.DetailAsync(id);

            if (realestate == null)
            {
                return NotFound();
            }

            var images = await _context.FilesToDatabases
                .Where(x => x.RealEstateId == id)
                .Select(y => new RealestateImageViewModel
                {
                    Image = y.ImageTitle,
                    ImageId = y.Id
                }).ToArrayAsync();

            //tuleb kasutada AddRange, et saada pildid vm kaasa
            //see on vaheinstants domaini ja vm vahel
            var vm = new RealEstateDetailsViewModel();

            vm.Id = realestate.Id;
            vm.Area = realestate.Area;
            vm.Location = realestate.Location;
            vm.RoomNumber = realestate.RoomNumber;
            vm.BuildingType = realestate.BuildingType;
            vm.CreatedAt = (DateTime)realestate.CreatedAt;
            vm.ModifiedAt = (DateTime)realestate.ModifiedAt;


            return View(vm);
        }

    }
}
