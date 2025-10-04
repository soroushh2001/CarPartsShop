using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MobileStore.Application.ViewModels.Products;

namespace CarPartsShop.Mvc.ViewComponents
{
    public class MainFilterProductsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(ProductFilterSpecification specification)
        {
            ViewBag.OrderBySelectList = new List<SelectListItem>()
        {
            new SelectListItem()
            {
                Value = "Desc",
                Text = "نزولی",
                Selected = specification.OrderBy == "Desc"
            },
            new SelectListItem()
            {
                Value = "Asc",
                Text = "صعودی",
                Selected = specification.OrderBy == "Asc"
            }
        }.ToList();

            ViewBag.SortBySelectList = new List<SelectListItem>()
        {
            new SelectListItem()
            {
                Value = "ModifiedDate",
                Text = "جدیدترین",
                Selected = specification.SortBy == "جدیدترین"
            },
            new SelectListItem()
            {
                Value  = "Price",
                Text = "قیمت",
                Selected = specification.SortBy == "Price"
            },
            new SelectListItem()
            {
                Value = "Title",
                Text = "عنوان",
                Selected = specification.SortBy == "Title"
            },
                new()
                {
                    Value = "BestSeller",
                    Text = "مقدار فروش",
                    Selected = specification.SortBy == "BestSeller"
                }
        };


            return View(specification);
        }
    }
}
