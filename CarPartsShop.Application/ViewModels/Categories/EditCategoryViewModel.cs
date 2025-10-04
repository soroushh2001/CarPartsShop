using System.ComponentModel.DataAnnotations;

namespace CarPartsShop.Application.ViewModels.Categories
{
    public class EditCategoryViewModel
    {
        public int Id { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد")]
        public string Title { get; set; } = null!;

        [Display(Name = "اسلاگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد")]
        public string Slug { get; set; } = null!;

        public string? ParentName { get; set; }

    }

    public enum EditCategoryResult
    {
        Success,
        InvalidSlug,
        InvalidTitle,
        NotFound
    }
}
