using CarPartsShop.Application.ViewModels.Categories;
using CarPartsShop.Application.ViewModels.Products;
using CarPartsShop.Domain.Entities.Shop;
using MobileStore.Application.ViewModels.Products;

namespace CarPartsShop.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<FilterProductViewModel> FilterProductAsync(ProductFilterSpecification specification);
        Task<CreateProductResult> CreateProductAsync(CreateProductViewModel createProductViewModel);
        Task AddCategoriesToProductAsync(List<int> categoriesIds, int productId);
        Task AddBrandToProductAsync(List<int> brandsIds, int productId);
        Task<EditProductViewModel?> GetProductToEditAsync(int productId);
        Task<EditProductResult> EditProductAsync(EditProductViewModel edit);
        Task<bool> ToggleProductStatusAsync(int productId);
        Task<List<ProductViewModel>> GetLatestProductAsync(int size = 5);
        Task<ProductViewModel?> GetProductBySlugAsync(string slug);
        Task<List<ProductViewModel>> GetRelatedProductsAsync(int productId, int size);
        Task<List<ProductViewModel>> GetBestSellerProductsAsync(int size = 5);
    }
}