using CarPartsShop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarPartsShop.Mvc.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ICarBrandService _carBrandService;

        public HeaderViewComponent(ICarBrandService carBrandService)
        {
            _carBrandService = carBrandService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.Brands = await _carBrandService.GetAllBrandsAsync(false);
            return View();
        }
    }
}
