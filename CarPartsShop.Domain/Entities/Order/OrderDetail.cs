using CarPartsShop.Domain.Entities.Common;
using CarPartsShop.Domain.Entities.Shop;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPartsShop.Domain.Entities.Order
{
    public class OrderDetail : BaseEntity
    {
        public int OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; }

        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        public int Price { get; set; }

        public int Count { get; set; }
    }
}
