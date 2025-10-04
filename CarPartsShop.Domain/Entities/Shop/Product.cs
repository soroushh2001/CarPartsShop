using CarPartsShop.Domain.Entities.Common;
using CarPartsShop.Domain.Entities.Order;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPartsShop.Domain.Entities.Shop
{
    public class Product : BaseEntity
    {
        public string Slug { get; set; }

        public string Title { get; set; }
   
        public string MainImage { get; set; }

        public int Price { get; set; }

        public string ShortDescription { get; set; }

        public string? Description { get; set; }

        public ProductQuality Quality { get; set; }

        public bool IsExisted { get; set; }

        public ICollection<ProductCarBrand>? ProductCarBrands { get; set; }

        public ICollection<ProductCategory>? ProductCategories { get; set; }
        public ICollection<OrderDetail>? OrderDetails { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }

    public enum ProductQuality
    {
        [Display(Name = "اصلی")]
        Original,
        [Display(Name = "شرکتی")]
        Corporate,
        [Display(Name = "متفرفه")]
        Others
    }
}
