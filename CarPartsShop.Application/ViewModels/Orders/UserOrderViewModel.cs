using CarPartsShop.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPartsShop.Application.ViewModels.Orders
{
    public class UserOrderViewModel
    {
        public int OrderId { get; set; }
        public string RefId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime? PaymentDate { get; set; }
        public int Sum { get; set; }
    }
}
