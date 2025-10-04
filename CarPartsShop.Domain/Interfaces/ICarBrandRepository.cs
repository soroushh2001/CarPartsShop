using CarPartsShop.Domain.Entities.Shop;

namespace CarPartsShop.Domain.Interfaces
{
    public interface ICarBrandRepository
    {
        IQueryable<CarBrand> GetAllBrands();
        Task<CarBrand?> GetCarBrandByIdAsync(int id);
        Task<bool> IsCarBrandTitleExisted(string title, int? id = null);
        Task<bool> IsCarBrandSlugExisted(string slug, int? id = null);
        Task AddCarBrandAsync(CarBrand carBrand);
        void UpdateCarBrand(CarBrand carBrand);
        Task SaveChangesAsync();

    }
}
