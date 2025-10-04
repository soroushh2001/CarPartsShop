using CarPartsShop.Application.Statics;
using CarPartsShop.Mvc.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarPartsShop.Mvc.Areas.Admin.Controllers
{
    [Authorize(Roles = $"{RoleConstants.Admin},{RoleConstants.Seller}")]
    public class HomeController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
