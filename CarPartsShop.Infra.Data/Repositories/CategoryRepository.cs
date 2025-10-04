using CarPartsShop.Domain.Entities.Shop;
using CarPartsShop.Domain.Interfaces;
using CarPartsShop.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CarPartsShop.Infra.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CarPartsShopDbContext _context;
        public CategoryRepository(CarPartsShopDbContext context)
        {
            _context = context;
        }

        public IQueryable<Category> GetAllCategories()
        {
            var query = _context.Categories.AsQueryable();
            return query;
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<Category?> GetCategoryBySlugAsync(string slug)
        {
            return await _context.Categories.FirstOrDefaultAsync(x=> x.Slug == slug);
        }

        public async Task<bool> IsCategoryTitleExisted(string title, int? id = null)
        {
            return await _context.Categories.AnyAsync(x=> x.Title == title && x.Id != id);
        }

        public async Task<bool> IsCategorySlugExisted(string slug, int? id = null)
        {
            return await _context.Categories.AnyAsync(x => x.Slug == slug && x.Id != id);
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public void UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
        }

        public async Task<List<Category>> GetAllMainCategories()
        {
            return await _context.Categories.Where(x => x.ParentId == null).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
