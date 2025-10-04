using CarPartsShop.Domain.Entities.Shop;

namespace CarPartsShop.Domain.Interfaces
{
    public interface IProductRepository
    {
        IQueryable<Product> GetAllProducts();
        Task<Product?> GetProductBySlug(string slug);
        Task<Product?> GetProductByIdAsync(int id);
        Task<bool> IsProductTitleExistedAsync(string title, int? id = null);
        Task<bool> IsProductSlugExistedAsync(string slug, int? id = null);
        Task AddProductAsync(Product product);
        void UpdateProduct(Product product);
        Task AddCategoriesToProductAsync(List<ProductCategory> categories);
        Task RemoveCategoriesFromProductAsync(int productId);
        Task AddCarBrandsToProductAsync(List<ProductCarBrand> productCarBrands);
        Task RemoveCarBrandsFromProductAsync(int productId);
        Task<List<int>> GetSelectedCategoriesIdsAsync(int productId);
        Task<List<int>> GetSelectedBrandsIdsAsync(int productId);
        Task<List<Product>> GetLatestProductAsync(int size);
        Task<List<Category>> GetSelectedCategoriesAsync(int productId);
        Task<List<CarBrand>> GetSelectedBrandsAsync(int productId);
        Task<List<int>> GetBrandIdsByProductIdAsync(int productId);
        Task<List<Product>> GetProductsByBrandIdsAsync(List<int> brandIds, int currentProductId, int size);
        Task<List<Product>> GetBestSellerProductsAsync(int size);
        Task SaveChangesAsync();
    }
}
