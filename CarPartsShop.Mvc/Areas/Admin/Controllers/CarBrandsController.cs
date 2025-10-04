using CarPartsShop.Application.Services.Interfaces;
using CarPartsShop.Application.Statics;
using CarPartsShop.Application.ViewModels.CarBrands;
using CarPartsShop.Mvc.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarPartsShop.Mvc.Areas.Admin.Controllers
{
    [Authorize(Roles = $"{RoleConstants.Admin},{RoleConstants.Seller}")]

    public class CarBrandsController : AdminBaseController
    {
        #region Constructor

        private readonly ICarBrandService _carBrandService;

        public CarBrandsController(ICarBrandService carBrandService)
        {
            _carBrandService = carBrandService;
        }

        #endregion

        #region BrandsList

        public async Task<IActionResult> Index()
        {
            return View(await _carBrandService.GetAllBrandsAsync());
        }

        #endregion

        #region CreateBrand

        [HttpGet]
        public IActionResult LoadCreateBrandModal(int? parentId, string? parentName)
        {
            return PartialView("_CreateBrand", new CreateCarBrandViewModel
            {
                ParentId = parentId,
                ParentName = parentName
            });
        }

        [HttpPost]
        public async Task<IActionResult> SubmitCreateBrandModal(CreateCarBrandViewModel create)
        {
            var result = await _carBrandService.CreateCarBrandAsync(create);

            switch (result)
            {
                case CreateCarBrandResult.Success:
                    return JsonHelper.JsonResponse(200, "عملیات با موفقیت انجام شد.");
                case CreateCarBrandResult.InvalidSlug:
                    return JsonHelper.JsonResponse(400, "این اسلاگ وجود دارد.");
                case CreateCarBrandResult.InvalidTitle:
                    return JsonHelper.JsonResponse(400, "این عنوان وجود دارد.");
            }
            return JsonHelper.JsonResponse(400, "عملیات با شکست مواجه شد");
        }


        #endregion

        #region EditBrand

        [HttpGet]
        public async Task<IActionResult> LoadEditBrandModal(int id)
        {
            return PartialView("_EditBrand", await _carBrandService.GetCarBrandToEditAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> SubmitEditBrandModal(EditCarBrandViewModel edit)
        {
            var result = await _carBrandService.EditCarBrandAsync(edit);

            switch (result)
            {
                case EditCarBrandResult.Success:
                    return JsonHelper.JsonResponse(200, "عملیات با موفقیت انجام شد.");
                case EditCarBrandResult.InvalidSlug:
                    return JsonHelper.JsonResponse(400, "این اسلاگ وجود دارد.");
                case EditCarBrandResult.InvalidTitle:
                    return JsonHelper.JsonResponse(400, "این عنوان وجود دارد.");
                case EditCarBrandResult.NotFound:
                    return JsonHelper.JsonResponse(400, "عملیات با شکست مواجه شد");
            }
            return JsonHelper.JsonResponse(400, "عملیات با شکست مواجه شد");
        }


        #endregion

        #region ToggleDeleteBrand

        [HttpGet]
        public async Task<IActionResult> ToggleDeleteBrand(int brandId)
        {
            var result = await _carBrandService.ToggleDeleteCarBrandAsync(brandId);
            if (result)
            {
                return JsonHelper.JsonResponse(200, "عملیات با موفقیت انجام شد");
            }
            return JsonHelper.JsonResponse(400, "عملیات با شکست مواجه شد");
        }

        #endregion
    }

}

