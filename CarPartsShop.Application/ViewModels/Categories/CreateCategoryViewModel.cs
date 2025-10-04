using System.ComponentModel.DataAnnotations;

namespace CarPartsShop.Application.ViewModels.Categories
{
    public class CreateCategoryViewModel
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد")]
        public string Title { get; set; } = null!;

        [Display(Name = "اسلاگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد")]
        public string Slug { get; set; } = null!;

        public int? ParentId { get; set; }

        public string? ParentName { get; set; }
    }
    
    public enum CreateCategoryResult
    {
        Success,
        InvalidSlug,
        InvalidTitle 
    }
}
