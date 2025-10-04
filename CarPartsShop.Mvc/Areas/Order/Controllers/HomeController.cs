using CarPartsShop.Application.Extensions;
using CarPartsShop.Application.Services.Interfaces;
using CarPartsShop.Application.Statics;
using CarPartsShop.Application.ViewModels.Orders;
using CarPartsShop.Application.ViewModels.Payment;
using CarPartsShop.Mvc.Helpers;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Text;
using System.Threading.Tasks;
using Method = RestSharp.Method;

namespace CarPartsShop.Mvc.Areas.Order.Controllers
{
    [Area("Order")]
    public class HomeController : Controller
    {
        #region Constructor

        private readonly IOrderService _orderService;
        private readonly IConfiguration _configuration;
        private readonly ICaptchaValidator _captchaValidator;


        public HomeController(IOrderService orderService, IConfiguration configuration, ICaptchaValidator captchaValidator)
        {
            _orderService = orderService;
            _configuration = configuration;
            _captchaValidator = captchaValidator;
        }

        #endregion

        #region AddToCart

        public async Task<IActionResult> AddToCart(int id,int count = 1)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return JsonHelper.JsonResponse(401, "برای افزودن محصول لطفا وارد شوید.");
            }

            await _orderService.AddToCart(User.GetCurrentUserId(), id, count);

            return JsonHelper.JsonResponse(200, "سبد خرید شما بروزرسانی شد");
        }


        #endregion

        #region Cart

        [HttpGet("cart")]
        [Authorize]
        public async Task<IActionResult> ShowCart()
        {
            return View(await _orderService.GetCartAsync(User.GetCurrentUserId()));
        }

        #endregion

        #region IncreaseDecreaseCartItem
        
        [HttpGet]
        public async Task<IActionResult> IncreaseDecreaseCartItem(int orderDetailsId, string op)
        {
            await _orderService.IncreaseDecreaseCartItemAsync(orderDetailsId, op, User.GetCurrentUserId());
            return JsonHelper.JsonResponse(200, "سبد خرید شما بروزرسانی شد");
        }

        #endregion

        #region RemoveItemFromCart

        [HttpGet]
        public async Task<IActionResult> RemoveItemFromCart(int orderDetailId)
        {
            await _orderService.RemoveItemFromCartAsync(orderDetailId,User.GetCurrentUserId());
            return JsonHelper.JsonResponse(200, "سبد خرید شما بروزرسانی شد");
        }

        #endregion

        #region CompleteOrderInformation

        [HttpGet("complete-order-info")]
        public IActionResult CompleteOrderInformation(int orderId)
        {
            ViewBag.OrderId = orderId;
            return View();
        }


        [HttpPost("complete-order-info")]
        public async Task<IActionResult> CompleteOrderInformation(CompleteOrderInformationViewModel orderInfo)
        {
            if (!await _captchaValidator.IsCaptchaPassedAsync(orderInfo.Captcha))
            {
                TempData[ToastrMessages.ErrorMessage] = "اعتبار سنجی Captcha با خطا مواجه شد لطفا مجدد تلاش کنید .";
                return View(orderInfo);
            }

            if (ModelState.IsValid)
            {
                await _orderService.CompleteOrderInformation(User.GetCurrentUserId(), orderInfo);
                return Redirect("/start-payment");
            }

            return View(orderInfo);
        }


        #endregion

        #region Payment

        [HttpGet("start-payment")]
        public async Task<IActionResult> StartPayment()
        {
            try
            {
                string authority = "";
                string callbackurl = $"{_configuration.GetValue<string>("Domain")}/verify-payment";
                var totalPrice = await _orderService.GetCurrentCartTotalPriceAsync(User.GetCurrentUserId());
                var amount = totalPrice * 10;
                string description = "پرداخت نهایی سبد خرید شما";
                string merchant = _configuration.GetValue<string>("Merchant");

                using (var client = new HttpClient())
                {
                    Application.ViewModels.Payment.RequestParameters parameters = new Application.ViewModels.Payment.RequestParameters(merchant, amount.ToString(), description, callbackurl, "", "");

                    var json = JsonConvert.SerializeObject(parameters);

                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(URLs.requestUrl, content);

                    string responseBody = await response.Content.ReadAsStringAsync();

                    JObject jo = JObject.Parse(responseBody);
                    string errorscode = jo["errors"].ToString();

                    JObject jodata = JObject.Parse(responseBody);
                    string dataauth = jodata["data"].ToString();


                    if (dataauth != "[]")
                    {
                        authority = jodata["data"]["authority"].ToString();

                        string gatewayUrl = URLs.gateWayUrl + authority;

                        return Redirect(gatewayUrl);
                    }
                    else
                    {
                        return BadRequest("error " + errorscode);
                    }
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpGet("verify-payment")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyPayment()
        {
            string authority = "";
            try
            {
                VerifyParameters parameters = new VerifyParameters();
                string callbackurl = $"{_configuration.GetValue<string>("Domain")}/user/verify-payment";
                var totalPrice = await _orderService.GetCurrentCartTotalPriceAsync(User.GetCurrentUserId());
                var amount = totalPrice * 10;

                if (HttpContext.Request.Query["Authority"] != "")
                {
                    authority = HttpContext.Request.Query["Authority"];
                }

                parameters.authority = authority;

                parameters.amount = amount.ToString();

                parameters.merchant_id = _configuration.GetValue<string>("Merchant");


                using (HttpClient client = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(parameters);

                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(URLs.verifyUrl, content);

                    string responseBody = await response.Content.ReadAsStringAsync();

                    JObject jodata = JObject.Parse(responseBody);

                    string data = jodata["data"].ToString();

                    JObject jo = JObject.Parse(responseBody);
                    if (HttpContext.Request.Query["Status"].ToString().ToLower() != "ok")
                    {
                        return View();
                    }

                    string errors = jo["errors"].ToString();

                    if (data != "[]")
                    {
                        // success payment section

                        string refid = jodata["data"]["ref_id"].ToString();
                        // complete payment
                        await _orderService.FinallyOrderAsync(User.GetCurrentUserId(), refid);
                        ViewBag.Id = refid;

                        return View();
                    }
                    else if (errors != "[]")
                    {
                        string errorscode = jo["errors"]["code"].ToString();

                        return BadRequest($"error code {errorscode}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return NotFound();
        }

        #endregion

        #region UserOrders

        [HttpGet("my-orders")]
        public async Task<IActionResult> UserOrders()
        {
            return View(await _orderService.GetUserFinallyOrdersAsync(HttpContext.User.GetCurrentUserId()));
        }

        [HttpGet("show-order/{orderId}")]
        public async Task<IActionResult> OrderDetails(int orderId)
        {
            var details = await _orderService.GetOrderDetailsByOrderIdAsync(orderId);

            if (details == null)
                return NotFound();

            ViewBag.OrderId = orderId;

            return View(details);
        }

        #endregion

    }
}
