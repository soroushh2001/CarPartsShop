using CarPartsShop.Domain.Entities.Shop;

namespace CarPartsShop.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        IQueryable<Category> GetAllCategories();
        Task<Category?> GetCategoryByIdAsync(int id);
        Task<Category?> GetCategoryBySlugAsync(string slug);
        Task<bool> IsCategoryTitleExisted(string title, int? id = null);
        Task<bool> IsCategorySlugExisted(string slug, int? id = null);
        Task AddCategoryAsync(Category category);
        void UpdateCategory(Category category);
        Task<List<Category>> GetAllMainCategories();
        Task SaveChangesAsync();
    }
}
