using CarPartsShop.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPartsShop.Domain.Entities.Shop
{
    public class ProductCarBrand : BaseEntity
    { 
        public int CarBrandId { get; set; }
        [ForeignKey(nameof(CarBrandId))]
        public CarBrand CarBrand { get; set; }

        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
    }
}
