using CarPartsShop.Application.ViewModels.Common;
using CarPartsShop.Application.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPartsShop.Application.ViewModels.Accounts
{
    public class FilterUserViewModel : Paging<UserViewModel>
    {
        public string? Search { get; set; }
        public string? Email { get; set; }

        public string? Role { get; set; }
        public SortUser Sort { get; set; }
    }
    public enum SortUser
    {
        [Display(Name = "جدیدترین")]
        Newest,
        [Display(Name = "قدیمی ترین")]
        Oldest
    }
}
