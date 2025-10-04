using CarPartsShop.Domain.Entities.Shop;
using CarPartsShop.Domain.Interfaces;
using CarPartsShop.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CarPartsShop.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CarPartsShopDbContext _context;

        public ProductRepository(CarPartsShopDbContext context)
        {
            _context = context;
        }

        public IQueryable<Product> GetAllProducts()
        {
            return _context.Products.Include(x => x.ProductCategories)
                .AsQueryable();
        }

        public async Task<Product?> GetProductBySlug(string slug)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Slug == slug);
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<bool> IsProductTitleExistedAsync(string title, int? id = null)
        {
            return await _context.Products.AnyAsync(x => x.Title == title && x.Id != id);
        }

        public async Task<bool> IsProductSlugExistedAsync(string slug, int? id = null)
        {
            return await _context.Products.AnyAsync(x => x.Slug == slug && x.Id != id);
        }

        public async Task AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
        }

        public async Task AddCategoriesToProductAsync(List<ProductCategory> categories)
        {
            await _context.ProductCategories.AddRangeAsync(categories);
        }

        public async Task RemoveCategoriesFromProductAsync(int productId)
        {
            _context.RemoveRange(await _context.ProductCategories.Where(x => x.ProductId == productId).ToListAsync());
        }

        public async Task AddCarBrandsToProductAsync(List<ProductCarBrand> productCarBrands)
        {
            await _context.ProductCarBrands.AddRangeAsync(productCarBrands);
        }

        public async Task RemoveCarBrandsFromProductAsync(int productId)
        {
            _context.RemoveRange(await _context.ProductCarBrands.Where(x => x.ProductId == productId).ToListAsync());
        }

        public async Task<List<int>> GetSelectedCategoriesIdsAsync(int productId)
        {
            return await _context.ProductCategories.Where(x => x.ProductId == productId).Select(x => x.CategoryId).ToListAsync();
        }

        public async Task<List<int>> GetSelectedBrandsIdsAsync(int productId)
        {
            return await _context.ProductCarBrands.Where(x => x.ProductId == productId).Select(x => x.CarBrandId).ToListAsync();
        }

        public async Task<List<Product>> GetLatestProductAsync(int size)
        {
            return await _context.Products.Where(x=> x.IsExisted).OrderByDescending(x => x.LastModifiedDate)
                .Take(size).ToListAsync();
        }

        public async Task<List<Category>> GetSelectedCategoriesAsync(int productId)
        {
            return await _context.ProductCategories.Where(x => x.ProductId == productId).Select(x => x.Category).ToListAsync();
        }

        public async Task<List<CarBrand>> GetSelectedBrandsAsync(int productId)
        {
            return await _context.ProductCarBrands.Where(x => x.ProductId == productId).Select(x => x.CarBrand).ToListAsync();
        }

        public async Task<List<int>> GetBrandIdsByProductIdAsync(int productId)
        {
            return await _context.ProductCarBrands
           .Where(pcb => pcb.ProductId == productId)
           .Select(pcb => pcb.CarBrandId)
           .ToListAsync();
        }

        public async Task<List<Product>> GetProductsByBrandIdsAsync(List<int> brandIds, int currentProductId, int size)
        {
            return await _context.Products.Where(p => p.Id != currentProductId && p.ProductCarBrands.Any(pcb => brandIds.Contains(pcb.CarBrandId)))
                .Take(size)
                .ToListAsync();
        }


        public async Task<List<Product>> GetBestSellerProductsAsync(int size)
        {
            return await _context.Products
                .OrderByDescending(p => p.OrderDetails
    .Where(od => od.Order.Status != Domain.Entities.Order.OrderStatus.WaitToPayment)
    .Sum(od => od.Count))

                .Take(size)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
