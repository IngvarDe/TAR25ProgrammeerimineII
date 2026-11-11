using Microsoft.AspNetCore.Mvc;
using ShopTARpe25.Core.Dto;
using ShopTARpe25.Core.ServiceInterface;
using ShopTARpe25.Data;
using ShopTARpe25.Models.RealEstate;

namespace ShopTARpe25.Controllers
{
    public class RealEstateController : Controller
    {
        private readonly ShopTARpe25Context _context;
        private readonly IRealEstateServices _realEstateServices;

        public RealEstateController
            (
                ShopTARpe25Context context,
                IRealEstateServices realEstateServices
            )
        {
            _context = context;
            _realEstateServices = realEstateServices;
        }

        public IActionResult Index()
        {
            var result = _context.Realestates
                .Select(x => new RealEstateIndexViewModel
                {
                    Id = x.Id,
                    Area = x.Area,
                    BuildingType = x.BuildingType,
                    RoomNumber = x.RoomNumber,
                });

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            RealEstateCreateUpdateViewModel result = new();

            return View("CreateUpdate", result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RealEstateCreateUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateUpdate", model);
            }

            var dto = new RealEstateDto
            {
                Area = model.Area,
                Address = model.Address,
                RoomNumber = model.RoomNumber,
                BuildingType = model.BuildingType,
            };

            var result = await _realEstateServices.Create(dto);

            if (result == null)
            {
                ModelState.AddModelError("", "Real estate creation failed.");
                return View(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var result = await _realEstateServices.DetailAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            var vm = new RealEstateCreateUpdateViewModel
            {
                Id = result.Id,
                Area = result.Area,
                Address = result.Address,
                RoomNumber = result.RoomNumber,
                BuildingType = result.BuildingType,
                CreatedAt = result.CreatedAt,
                ModifiedAt = result.ModifiedAt,
            };

            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(RealEstateCreateUpdateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateUpdate", vm);
            }

            var dto = new RealEstateDto
            {
                Id = vm.Id,
                Area = vm.Area,
                Address = vm.Address,
                RoomNumber = vm.RoomNumber,
                BuildingType = vm.BuildingType,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt
            };

            var result = await _realEstateServices.Update(dto);

            if (result == null)
            {
                ModelState.AddModelError("", "Real estate update failed.");
                return View(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _realEstateServices.DetailAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            var vm = new RealEstateDeleteViewModel
            {
                Id = result.Id,
                Area = result.Area,
                Address = result.Address,
                RoomNumber = result.RoomNumber,
                BuildingType = result.BuildingType,
                CreatedAt = result.CreatedAt,
                ModifiedAt = result.ModifiedAt,
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var realEstate = await _realEstateServices.Delete(id);

            if (realEstate == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _realEstateServices.DetailAsync(id);

            if (result == null)
            {
                ModelState.AddModelError("", "Real estate detail failed.");
                return View(nameof(Index));
            }

            var vm = new RealEstateDetailsViewModel
            {
                Id = result.Id,
                Area = result.Area,
                Address = result.Address,
                RoomNumber = result.RoomNumber,
                BuildingType = result.BuildingType,
                CreatedAt = result.CreatedAt,
                ModifiedAt = result.ModifiedAt,
            };

            return View(vm);
        }
    }
}
