using CarPartsShop.Domain.Entities.Shop;

namespace CarPartsShop.Application.ViewModels.Products
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Slug { get; set; }

        public string Title { get; set; }

        public string MainImage { get; set; }

        public int Price { get; set; }

        public string ShortDescription { get; set; }

        public string? Description { get; set; }

        public List<CarBrand> CarBrands { get; set; }

        public List<Category> Categories { get; set; }

        public bool IsExisted { get; set; }
        public ProductQuality ProductQuality { get; set; }
    }
}
