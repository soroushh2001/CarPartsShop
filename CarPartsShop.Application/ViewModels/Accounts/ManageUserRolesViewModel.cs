using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPartsShop.Application.ViewModels.Accounts
{
    public class ManageUserRolesViewModel
    {
        public string Email { get; set; }

        [Display(Name = "نقش")]
        [Required(ErrorMessage = "لطفا حداقل یک {0} را انتخاب کنید")]
        [MinLength(1, ErrorMessage = "لطفا حداقل یک {0} را انتخاب کنید")]
        public List<string> SelectedRoles { get; set; } 
    }
}
