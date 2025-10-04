using CarPartsShop.Application.Extensions;
using CarPartsShop.Application.Services.Interfaces;
using CarPartsShop.Application.ViewModels.CarBrands;
using CarPartsShop.Domain.Entities.Shop;
using CarPartsShop.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarPartsShop.Application.Services.Implementations
{
    public class CarBrandService : ICarBrandService
    {
        private readonly ICarBrandRepository _carBrandRepository;
        public CarBrandService(ICarBrandRepository carBrandRepository)
        {
            _carBrandRepository = carBrandRepository;
        }

        public async Task<List<CarBrandViewModel>> GetAllBrandsAsync(bool? isDeleted = null)
        {
            var brands = _carBrandRepository.GetAllBrands();
            if (isDeleted != null)
            {
                brands = brands.Where(x => x.IsDeleted == isDeleted);
            }
            return await brands.Select(x => new CarBrandViewModel
            {
                Slug = x.Slug,
                Title = x.Title,
                Id = x.Id,
                IsDeleted = x.IsDeleted,
                ParentId = x.ParentId
            }).ToListAsync();
        }

        public async Task<CreateCarBrandResult> CreateCarBrandAsync(CreateCarBrandViewModel create)
        {
            var checkTitle = await _carBrandRepository.IsCarBrandTitleExisted(create.Title);
            if (checkTitle)
                return CreateCarBrandResult.InvalidTitle;

            var checkSlug = await _carBrandRepository.IsCarBrandSlugExisted(create.Slug);
            if (checkSlug)
                return CreateCarBrandResult.InvalidSlug;
            var newBrand = new CarBrand
            {
                Slug = create.Slug.GenerateSlug(),
                Title = create.Title,
                ParentId= create.ParentId,
            };
            await _carBrandRepository.AddCarBrandAsync(newBrand);
            await _carBrandRepository.SaveChangesAsync();
            return CreateCarBrandResult.Success;
        }

        public async Task<EditCarBrandViewModel?> GetCarBrandToEditAsync(int id)
        {
            var brandToEdit = await _carBrandRepository.GetCarBrandByIdAsync(id);
            if (brandToEdit == null)
                return null;
            return new()
            {
                Title = brandToEdit.Title,
                Slug = brandToEdit.Slug,
                Id = id
            };
        }

        public async Task<EditCarBrandResult> EditCarBrandAsync(EditCarBrandViewModel edit)
        {
            var brandToEdit = await _carBrandRepository.GetCarBrandByIdAsync(edit.Id);
            if (brandToEdit == null)
                return EditCarBrandResult.NotFound;
            var checkTitle = await _carBrandRepository.IsCarBrandTitleExisted(edit.Title, edit.Id);
            if (checkTitle)
                return EditCarBrandResult.InvalidTitle;

            var checkSlug = await _carBrandRepository.IsCarBrandSlugExisted(edit.Slug, edit.Id);
            if (checkSlug)
                return EditCarBrandResult.InvalidSlug;

            brandToEdit.Slug = edit.Slug.GenerateSlug();
            brandToEdit.Title = edit.Title;

            _carBrandRepository.UpdateCarBrand(brandToEdit);
            await _carBrandRepository.SaveChangesAsync();
            return EditCarBrandResult.Success;
        }

        public async Task<bool> ToggleDeleteCarBrandAsync(int brandId)
        {
            var carBrand = await _carBrandRepository.GetCarBrandByIdAsync(brandId);
            if (carBrand == null) return false;
            carBrand.IsDeleted = !carBrand.IsDeleted;
            _carBrandRepository.UpdateCarBrand(carBrand);
            await _carBrandRepository.SaveChangesAsync();
            return true;
        }
    }
}
