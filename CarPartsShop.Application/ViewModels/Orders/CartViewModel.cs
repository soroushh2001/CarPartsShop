using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPartsShop.Application.ViewModels.Orders
{
    public class CartViewModel
    {
        public List<CartItemViewModel> Items { get; set; }
        public int OrderId { get; set; }
        public int Sum { get; set; }
    }
}
