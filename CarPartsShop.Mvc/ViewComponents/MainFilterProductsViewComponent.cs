using CarPartsShop.Application.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarPartsShop.Mvc.ViewComponents
{
    public class MainFilterProductsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(FilterProductViewModel filter)
        {
            return View(filter);
        }
    }
}
