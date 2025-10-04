using CarPartsShop.Application.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPartsShop.Application.ViewModels.Orders
{
    public class CompleteOrderInformationViewModel : CaptchaViewModel
    {
        [Display(Name = "نام و نام خانوادگی تحویل گیرنده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد")]
        public string RecipientName { get; set; }

        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد")]
        public string Address { get; set; }


        [Display(Name = "کد پستی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد")]
        public string ZipCode { get; set; }


        [Display(Name = "تلفن همراه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(20, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد")]
        public string PhoneNumber { get; set; }

    }
}
