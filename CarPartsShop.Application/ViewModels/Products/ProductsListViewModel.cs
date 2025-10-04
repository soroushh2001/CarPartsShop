using CarPartsShop.Domain.Entities.Shop;

namespace CarPartsShop.Application.ViewModels.Products
{
    public class ProductsListViewModel
    {
        public int Id { get; set; }

        public string Slug { get; set; }
        public string Title { get; set; }

        public int Price { get; set; }

        public DateTime CreatedDate { get; set; }
        
        public DateTime LastModifiedDate { get; set; }

        public List<Category> Categories { get; set; }

        public ProductQuality Quality { get; set; }

        public bool IsExisted { get; set; }
        public string MainImage { get; set; }

    }
}
