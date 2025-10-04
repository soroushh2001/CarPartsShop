using CarPartsShop.Application.Extensions;
using CarPartsShop.Application.Services.Interfaces;
using CarPartsShop.Mvc.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CarPartsShop.Mvc.Areas.Order.ViewComponents
{
    public class TotalPriceInCompleteInformation : ViewComponent
    {
        private readonly IOrderService _orderService;
        public TotalPriceInCompleteInformation(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _orderService.GetCurrentCartTotalPriceAsync(HttpContext.User.GetCurrentUserId()));
        }
    }
}
