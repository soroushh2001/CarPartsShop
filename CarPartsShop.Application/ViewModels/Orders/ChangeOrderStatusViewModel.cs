using CarPartsShop.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPartsShop.Application.ViewModels.Orders
{
    public class ChangeOrderStatusViewModel
    {
        public string RefCode { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
