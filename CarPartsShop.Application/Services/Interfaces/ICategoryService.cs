using CarPartsShop.Application.ViewModels.CarBrands;
using CarPartsShop.Application.ViewModels.Categories;

namespace CarPartsShop.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryViewModel>> GetAllCategoriesAsync(bool? isDeleted = null);
        Task<CategoryViewModel?> GetCategoryByIdAsync(int id);
        Task<CategoryViewModel?> GetCategoryBySlugAsync(string slug);
        Task<CreateCategoryResult> CreateCategoryAsync(CreateCategoryViewModel createCategoryViewModel);
        Task<EditCategoryViewModel?> GetCategoryToEditAsync(int id);
        Task<EditCategoryResult> EditCategoryAsync(EditCategoryViewModel editCategoryViewModel);
        Task<bool> ToggleDeleteCategoryAsync(int categoryId);
        Task<List<CategoryViewModel>> GetAllMainCategoriesAsync();
    }
}
