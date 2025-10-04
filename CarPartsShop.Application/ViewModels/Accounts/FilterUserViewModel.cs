using CarPartsShop.Application.ViewModels.Common;
using CarPartsShop.Application.ViewModels.Products;
using MobileStore.Application.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPartsShop.Application.ViewModels.Accounts
{
    public class FilterUserViewModel
    {
        public PaginatedListViewModel<UserViewModel>? Users { get; set; }
        public UserFilterSpecification? Specification { get; set; }

    }
}
