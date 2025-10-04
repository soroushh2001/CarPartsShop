using CarPartsShop.Application.ViewModels.Common;
using System.ComponentModel.DataAnnotations;

namespace MobileStore.Application.ViewModels.Accounts;

public class ForgotUserPasswordViewModel : CaptchaViewModel
{
    [Display(Name = "ایمیل")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
    [EmailAddress(ErrorMessage = "ساختار ایمیل صحیح نمی باشد.")]
    [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر داشته باشد.")]
    public string Email { get; set; } = null!;
}