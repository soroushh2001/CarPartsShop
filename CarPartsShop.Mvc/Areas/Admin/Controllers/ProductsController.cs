using CarPartsShop.Application.Services.Implementations;
using CarPartsShop.Application.Services.Interfaces;
using CarPartsShop.Application.Statics;
using CarPartsShop.Application.ViewModels.Products;
using CarPartsShop.Mvc.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarPartsShop.Mvc.Areas.Admin.Controllers
{
    [Authorize(Roles = $"{RoleConstants.Admin},{RoleConstants.Seller}")]
    public class ProductsController : AdminBaseController
    {
        #region Constructor

        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ICarBrandService _carBrandService;

        public ProductsController(IProductService productService, ICategoryService categoryService, ICarBrandService carBrandService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _carBrandService = carBrandService;
        }

        #endregion

        #region ProductsList

        public async Task<IActionResult> Index(FilterProductViewModel filter)
        {
            filter.TakeEntity = 9;

            var categories = await _categoryService.GetAllMainCategoriesAsync();

            ViewBag.Categories = categories.Select(x => new SelectListItem()
            {
                Text = x.Title,
                Value = x.Title,
                Selected = x.Title == filter.CategoryTitle
            });
            
            return View(await _productService.FilterProductAsync(filter));
        }

        #endregion

        #region CreateProduct

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            ViewBag.Categories = await _categoryService.GetAllCategoriesAsync(false);
            ViewBag.CarBrands = await _carBrandService.GetAllBrandsAsync(false);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.CreateProductAsync(model);
                switch (result)
                {
                    case CreateProductResult.Success:
                        TempData[ToastrMessages.SuccessMessage] = "عملیات با موفقیت انجام شد";
                        return RedirectToAction("Index", "Products", new { area = "Admin" });
                    case CreateProductResult.InvalidTitle:
                        ModelState.AddModelError(string.Empty, "عنوان وارد شده وجود دارد.");
                        break;
                    case CreateProductResult.InvalidImage:
                        ModelState.AddModelError(string.Empty, "فرمت عکس وارد شده صحیح نیست.");
                        break;
                }
            }
            ViewBag.Categories = await _categoryService.GetAllCategoriesAsync(false);
            ViewBag.CarBrands = await _carBrandService.GetAllBrandsAsync(false);

            return View(model);
        }

        #endregion

        #region EditProduct

        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _productService.GetProductToEditAsync(id);
            if (product == null)
                return NotFound();
            ViewBag.Categories = await _categoryService.GetAllCategoriesAsync(false);
            ViewBag.CarBrands = await _carBrandService.GetAllBrandsAsync(false);

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(EditProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.EditProductAsync(model);
                switch (result)
                {
                    case EditProductResult.Success:
                        TempData[ToastrMessages.SuccessMessage] = "عملیات با موفقیت انجام شد";
                        return RedirectToAction("Index", "Products", new { area = "Admin" });
                    case EditProductResult.InvalidTitle:
                        ModelState.AddModelError(string.Empty, "عنوان وارد شده وجود دارد.");
                        break;
                    case EditProductResult.InvalidImage:
                        ModelState.AddModelError(string.Empty, "فرمت عکس وارد شده صحیح نیست.");
                        break;
                    case EditProductResult.NotFound:
                        return NotFound();
                }
            }

            ViewBag.Categories = await _categoryService.GetAllCategoriesAsync(false);
            ViewBag.CarBrands = await _carBrandService.GetAllBrandsAsync(false);

            return View(model);
        }

        #endregion

        #region ToggleProductStatus

        [HttpGet]
        public async Task<IActionResult> ToggleProductStatus(int productId)
        {
            var result = await _productService.ToggleProductStatusAsync(productId);
            if (result)
                return JsonHelper.JsonResponse(200, "عملیات با موفقیت انجام شد");
            return JsonHelper.JsonResponse(400, "عملیات با شکست مواجه شد");

        }

        #endregion       

    }
}
