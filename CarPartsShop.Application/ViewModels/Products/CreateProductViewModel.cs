using CarPartsShop.Domain.Entities.Shop;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CarPartsShop.Application.ViewModels.Products
{
    public class CreateProductViewModel
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد")]
        public string Title { get; set; }

        [Display(Name = "عکس اصلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public IFormFile MainImage { get; set; }

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Price { get; set; }

        [Display(Name = "توضیحات کوتاه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ShortDescription { get; set; }

        [Display(Name = "توضیحات اصلی")]
        public string? Description { get; set; }

        [Display(Name = "وضعیت")]
        public bool IsExisted { get; set; }

        [Display(Name = "دسته بندی ها")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public List<int> SelecetCategoryIds { get; set; }

        [Display(Name = "نوع محصول")]
        public ProductQuality ProductQuality { get; set; }

        [Display(Name = "مدل خودرو")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public List<int> SelecetedBrandsIds { get; set; }
    }

    public enum CreateProductResult
    {
        Success,
        InvalidTitle,
        InvalidImage
    }

}
