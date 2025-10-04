using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarPartsShop.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
