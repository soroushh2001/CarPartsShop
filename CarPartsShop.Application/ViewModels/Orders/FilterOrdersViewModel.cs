using CarPartsShop.Application.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPartsShop.Application.ViewModels.Orders
{
    public class FilterOrdersViewModel
    {
        public PaginatedListViewModel<UserOrderViewModel> Orders { get; set; }
        public OrderFilterSpecification Specification { get; set; }  
    }
}
