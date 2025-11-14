using CarPartsShop.Application.Services.Interfaces;
using CarPartsShop.Application.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;

namespace CarPartsShop.Mvc.ViewComponents
{
    public class FilterProductsRightSideBarViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        public FilterProductsRightSideBarViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync(FilterProductViewModel filter)
        {
            ViewBag.Categories = await _categoryService.GetAllCategoriesAsync(false);

            return View(filter);
        }
    }
}
