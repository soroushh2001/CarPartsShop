using CarPartsShop.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPartsShop.Domain.Entities.Shop
{
    public class CarBrand : BaseEntity
    {
        [MaxLength(250)]
        public string Title { get; set; }

        [MaxLength(250)]
        public string Slug { get; set; }

        public int? ParentId { get; set; }
        [ForeignKey(nameof(ParentId))]
        public ICollection<CarBrand>? CarBrands { get; set; }

        public ICollection<ProductCarBrand>? ProductCarBrands { get; set; }
       
    }
}
