using CarPartsShop.Application.ViewModels.Common;
using System.ComponentModel.DataAnnotations;

namespace CarPartsShop.Application.ViewModels.Accounts
{
    public class RegisterUserViewModel : CaptchaViewModel
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [EmailAddress(ErrorMessage = "ساختار ایمیل صحیح نمی باشد.")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر داشته باشد.")]
        public string Email { get; set; } = null!;

        [Display(Name = "کلمه عبور")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر داشته باشد.")]
        public string Password { get; set; } = null!;

        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "کلمه عبور و تکرار آن با یکدیگر مغایرت دارند.")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر داشته باشد.")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
