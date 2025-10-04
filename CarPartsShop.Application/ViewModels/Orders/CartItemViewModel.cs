using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPartsShop.Application.ViewModels.Orders
{
    public class CartItemViewModel
    {
        public int OrderDetailId { get; set; }
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public string MainImage { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        public int TotalPrice => Price * Count;
    }
}
