using CarPartsShop.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPartsShop.Domain.Entities.Shop
{
    public class ProductCategory : BaseEntity
    {
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]    
        public Category Category { get; set; }

        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
    }
}
