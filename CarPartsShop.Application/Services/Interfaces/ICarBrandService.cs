using CarPartsShop.Application.ViewModels.CarBrands;
using CarPartsShop.Application.ViewModels.Categories;

namespace CarPartsShop.Application.Services.Interfaces
{
    public interface ICarBrandService 
    {
        Task<List<CarBrandViewModel>> GetAllBrandsAsync(bool? isDeleted = null);
        Task<CreateCarBrandResult> CreateCarBrandAsync(CreateCarBrandViewModel create);
        Task<EditCarBrandViewModel?> GetCarBrandToEditAsync(int id);
        Task<EditCarBrandResult> EditCarBrandAsync(EditCarBrandViewModel edit);
        Task<bool> ToggleDeleteCarBrandAsync(int brandId);
    }
}
