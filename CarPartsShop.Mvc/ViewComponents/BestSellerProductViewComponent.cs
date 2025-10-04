using CarPartsShop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarPartsShop.Mvc.ViewComponents
{
    public class BestSellerProductViewComponent : ViewComponent
    {
        private readonly IProductService _productService;
        public BestSellerProductViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _productService.GetBestSellerProductsAsync(4));
        }
    }
}
