using CarPartsShop.Application.Extensions;
using CarPartsShop.Application.Services.Interfaces;
using CarPartsShop.Application.ViewModels.Comments;
using CarPartsShop.Application.ViewModels.Products;
using CarPartsShop.Mvc.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarPartsShop.Mvc.Controllers
{
    public class ProductsController : Controller
    {
        #region Constructor

        private readonly IProductService _productService;
        private readonly ICommentService _commentService;
        public ProductsController(IProductService productService, ICommentService commentService)
        {
            _productService = productService;
            _commentService = commentService;
        }

        #endregion

        #region FilterProducts

        [HttpGet("products")]
        [HttpGet("products/{BrandTitle?}")]
        public async Task<IActionResult> Index(FilterProductViewModel filter)
        {
            filter.TakeEntity = 9;
            return View(await _productService.FilterProductAsync(filter));
        }

        #endregion

        #region Details

        [HttpGet("products/details/{slug}")]
        public async Task<IActionResult> Details(string slug)
        {
            return View(await _productService.GetProductBySlugAsync(slug));
        }

        #endregion

        #region Comments


        [HttpPost]
        public async Task<IActionResult> AddComment(CreateCommentViewModel create)
        {
            if (create.Message == null)
            {
                TempData[ToastrMessages.ErrorMessage] = "لطفا نظر خود را وارد کنید";
                return RedirectToAction("Details","Products", new { slug=create.ProductSlug});
            }

            await _commentService.CreateComment(create,HttpContext.User.GetCurrentUserId());

            TempData[ToastrMessages.SuccessMessage] = "نظر شما با موفقیت ثبت شد";
            return RedirectToAction("Details", new { slug = create.ProductSlug });
        }

        #endregion
    }
}
