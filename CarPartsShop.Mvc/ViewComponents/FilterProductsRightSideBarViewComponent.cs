using CarPartsShop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MobileStore.Application.ViewModels.Products;

namespace CarPartsShop.Mvc.ViewComponents
{
    public class FilterProductsRightSideBarViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        public FilterProductsRightSideBarViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync(ProductFilterSpecification specification)
        {
            ViewBag.Categories = await _categoryService.GetAllCategoriesAsync(false);

            return View(specification);
        }
    }
}
