using CarPartsShop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarPartsShop.Mvc.Areas.Order.ViewComponents
{
    public class CartItemsInCompleteInformationViewComponent : ViewComponent
    {
        private readonly IOrderService _orderService;
        public CartItemsInCompleteInformationViewComponent(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int orderId)
        {
            return View(await _orderService.GetCartItemsByOrderIdAsync(orderId));
        }
    }
}
