using CarPartsShop.Application.Extensions;
using CarPartsShop.Application.Services.Interfaces;
using CarPartsShop.Application.ViewModels.CarBrands;
using CarPartsShop.Application.ViewModels.Categories;
using CarPartsShop.Domain.Entities.Shop;
using CarPartsShop.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarPartsShop.Application.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoriesAsync(bool? isDeleted = null)
        {
            var categories = _categoryRepository.GetAllCategories();

            if (isDeleted != null)
                categories = categories.Where(c => c.IsDeleted == isDeleted);

            return await categories.Select(x => new CategoryViewModel
            {
                Id = x.Id,
                ParentId = x.ParentId,
                Slug = x.Slug,
                Title = x.Title,
                IsDeleted = x.IsDeleted
            }).ToListAsync();
        }

        public async Task<CategoryViewModel?> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);

            if (category == null)
                return null;

            return new CategoryViewModel
            {
                Id = category.Id,
                Title = category.Title,
                ParentId = category.ParentId,
                Slug = category.Slug,
            };
        }

        public async Task<CategoryViewModel?> GetCategoryBySlugAsync(string slug)
        {
            var category = await _categoryRepository.GetCategoryBySlugAsync(slug);

            if (category == null)
                return null;

            return new CategoryViewModel
            {
                Id = category.Id,
                Title = category.Title,
                ParentId = category.ParentId,
                Slug = category.Slug,
            };
        }

        public async Task<CreateCategoryResult> CreateCategoryAsync(CreateCategoryViewModel createCategoryViewModel)
        {
            var checkTitle = await _categoryRepository.IsCategoryTitleExisted(createCategoryViewModel.Title);
            if (checkTitle)
                return CreateCategoryResult.InvalidTitle;

            var checkSlug = await _categoryRepository.IsCategorySlugExisted(createCategoryViewModel.Slug);
            if (checkSlug)
                return CreateCategoryResult.InvalidSlug;

            var newCategory = new Category
            {
                Title = createCategoryViewModel.Title,
                Slug = createCategoryViewModel.Slug.GenerateSlug(),
                ParentId = createCategoryViewModel.ParentId,
            };

            await _categoryRepository.AddCategoryAsync(newCategory);
            await _categoryRepository.SaveChangesAsync();
            return CreateCategoryResult.Success;
        }

        public async Task<EditCategoryViewModel?> GetCategoryToEditAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null) return null;
            return new EditCategoryViewModel
            {
                Id = category.Id,
                Slug = category.Slug,
                Title = category.Title,
            };
        }

        public async Task<EditCategoryResult> EditCategoryAsync(EditCategoryViewModel editCategoryViewModel)
        {
            var categoryToEdit = await _categoryRepository.GetCategoryByIdAsync(editCategoryViewModel.Id);

            if (categoryToEdit == null)
                return EditCategoryResult.NotFound;

            var checkTitle = await _categoryRepository.IsCategoryTitleExisted(editCategoryViewModel.Title,editCategoryViewModel.Id);
            if (checkTitle)
                return EditCategoryResult.InvalidTitle;

            var checkSlug = await _categoryRepository.IsCategorySlugExisted(editCategoryViewModel.Slug, editCategoryViewModel.Id);
            if (checkSlug)
                return EditCategoryResult.InvalidSlug;

            categoryToEdit.Title = editCategoryViewModel.Title;
            categoryToEdit.Slug = editCategoryViewModel.Slug;
            categoryToEdit.LastModifiedDate = DateTime.Now;

            _categoryRepository.UpdateCategory(categoryToEdit);
            await _categoryRepository.SaveChangesAsync();
            return EditCategoryResult.Success;
        }

        public async Task<bool> ToggleDeleteCategoryAsync(int categoryId)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
            if (category == null) return false;
            category.IsDeleted = !category.IsDeleted;
            _categoryRepository.UpdateCategory(category);
            await _categoryRepository.SaveChangesAsync();
            return true;
        }

        public async Task<List<CategoryViewModel>> GetAllMainCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllMainCategories();
            return categories.Select(x => new CategoryViewModel
            {
                Id = x.Id,
                Slug = x.Slug,
                Title = x.Title,
                ParentId = x.ParentId,
                IsDeleted = x.IsDeleted,
            }).ToList();
        }
    }
}
