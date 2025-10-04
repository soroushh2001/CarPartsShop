using CarPartsShop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarPartsShop.Mvc.ViewComponents
{
    public class SameProductsViewComponent : ViewComponent
    {
        private readonly IProductService _productService;
        public SameProductsViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int productId)
        {
            return View(await _productService.GetRelatedProductsAsync(productId,5));
        }
    }
}
