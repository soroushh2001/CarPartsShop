using CarPartsShop.Application.Services.Interfaces;
using CarPartsShop.Application.Statics;
using CarPartsShop.Application.ViewModels.Orders;
using CarPartsShop.Mvc.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarPartsShop.Mvc.Areas.Admin.Controllers
{
    [Authorize(Roles = $"{RoleConstants.Admin},{RoleConstants.Seller}")]

    public class OrdersController : AdminBaseController
    {
        #region Constructor
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        #endregion

        #region OrdersList
        [HttpGet]
        public async Task<IActionResult> Index(FilterOrdersViewModel filter)
        {
            filter.TakeEntity = 10;
            return View(await _orderService.FilterOrdersAsync(filter));
        }

        #endregion

        #region ChangeOrderStatus

        [HttpGet]
        public async Task<IActionResult> LoadChangeOrderStatusModal(string refCode)
        {
            var model = await _orderService.GetCurrentOrderStatusToChangeAsync(refCode);
            return PartialView("_ChangeOrderStatus", model);
        }


        [HttpPost]
        public async Task<IActionResult> SubmitChangeOrderStatusModal(ChangeOrderStatusViewModel changeOrderStatus)
        {
            var result = await _orderService.ChangeOrderStatusAsync(changeOrderStatus);
            if (result)
            {
                return JsonHelper.JsonResponse(200, "عملیات با موفقیت مواجه شد");

            }
            return JsonHelper.JsonResponse(400, "عملیات با شکست مواجه شد");

        }

        #endregion

        #region RecipientInfo

        [HttpGet]
        public async Task<IActionResult> LoadRecipientInfoModal(string refCode)
        {
            var recipient = await _orderService.GetRecipientInfoAsync(refCode);

            if (recipient == null)
                return NotFound();

            return PartialView("_RecipientInfo", recipient);
        }

        #endregion

        #region OrderDetails

        public async Task<IActionResult> ShowOrder(int orderId, string refCode)
        {
            var details = await _orderService.GetOrderDetailsByOrderIdAsync(orderId);
            if (details == null)
                return NotFound();

            ViewBag.RefCode = refCode;
            return View(details);
        }


        #endregion
    }
}
