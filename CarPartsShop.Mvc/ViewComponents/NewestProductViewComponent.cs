using CarPartsShop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarPartsShop.Mvc.ViewComponents
{
    public class NewestProductViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public NewestProductViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _productService.GetLatestProductAsync(4));
        }
    }
}
