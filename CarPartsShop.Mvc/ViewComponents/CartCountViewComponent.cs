using CarPartsShop.Application.Extensions;
using CarPartsShop.Application.Services.Interfaces;
using CarPartsShop.Mvc.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CarPartsShop.Mvc.ViewComponents
{
    public class CartCountViewComponent : ViewComponent
    {
        private readonly IOrderService _orderService;

        public CartCountViewComponent(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(await _orderService.GetCurrentCartItemsCountAsync(HttpContext.User.GetCurrentUserId()));

            }
            return View(0);
        }
    }
}
