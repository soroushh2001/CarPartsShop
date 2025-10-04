using CarPartsShop.Application.Services.Interfaces;
using CarPartsShop.Application.Statics;
using CarPartsShop.Application.ViewModels.Categories;
using CarPartsShop.Mvc.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarPartsShop.Mvc.Areas.Admin.Controllers
{
    [Authorize(Roles = $"{RoleConstants.Admin},{RoleConstants.Seller}")]
    public class CategoriesController : AdminBaseController
    {
        #region Constructor

        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        #endregion

        #region CategoriesList

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _categoryService.GetAllCategoriesAsync());
        }


        #endregion

        #region CreateCategory

        [HttpGet]
        public IActionResult LoadCreateCategoryModal(int? parentId, string? parentName)
        {
            return PartialView("_CreateCategory", new CreateCategoryViewModel
            {
                ParentId = parentId,
                ParentName = parentName
            });
        }

        [HttpPost]
        public async Task<IActionResult> SubmitCreateCategoryModal(CreateCategoryViewModel create)
        {
            var result = await _categoryService.CreateCategoryAsync(create);

            switch (result)
            {
                case CreateCategoryResult.Success:
                    return JsonHelper.JsonResponse(200, "عملیات با موفقیت انجام شد.");
                case CreateCategoryResult.InvalidSlug:
                    return JsonHelper.JsonResponse(400, "این اسلاگ وجود دارد.");
                case CreateCategoryResult.InvalidTitle:
                    return JsonHelper.JsonResponse(400, "این عنوان وجود دارد.");
            }
            return JsonHelper.JsonResponse(400, "عملیات با شکست مواجه شد");
        }

        #endregion

        #region EditCategory

        [HttpGet]
        public async Task<IActionResult> LoadEditCategoryModal(int id)
        {
            return PartialView("_EditCategory", await _categoryService.GetCategoryToEditAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> SubmitEditCategoryModal(EditCategoryViewModel edit)
        {
            var result = await _categoryService.EditCategoryAsync(edit);

            switch (result)
            {
                case EditCategoryResult.Success:
                    return JsonHelper.JsonResponse(200, "عملیات با موفقیت انجام شد.");
                case EditCategoryResult.InvalidSlug:
                    return JsonHelper.JsonResponse(400, "این اسلاگ وجود دارد.");
                case EditCategoryResult.InvalidTitle:
                    return JsonHelper.JsonResponse(400, "این عنوان وجود دارد.");
                case EditCategoryResult.NotFound:
                    return JsonHelper.JsonResponse(400, "عملیات با شکست مواجه شد");
            }
            return JsonHelper.JsonResponse(400, "عملیات با شکست مواجه شد");
        }

        #endregion

        #region ToggleDeleteCategory

        [HttpGet]
        public async Task<IActionResult> ToggleDeleteCategory(int categoryId)
        {
            var result = await _categoryService.ToggleDeleteCategoryAsync(categoryId);
            if (result)
            {
                return JsonHelper.JsonResponse(200, "عملیات با موفقیت انجام شد");
            }
            return JsonHelper.JsonResponse(400, "عملیات با شکست مواجه شد");
        }

        #endregion
    }
}
