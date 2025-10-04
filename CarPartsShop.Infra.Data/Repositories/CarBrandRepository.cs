using CarPartsShop.Domain.Entities.Shop;
using CarPartsShop.Domain.Interfaces;
using CarPartsShop.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPartsShop.Infra.Data.Repositories
{
    public class CarBrandRepository : ICarBrandRepository
    {
        private readonly CarPartsShopDbContext _context;

        public CarBrandRepository(CarPartsShopDbContext context)
        {
            _context = context;
        }

        public IQueryable<CarBrand> GetAllBrands()
        {
            return _context.CarBrands.AsQueryable();
        }

        public async Task<CarBrand?> GetCarBrandByIdAsync(int id)
        {
            return await _context.CarBrands.FindAsync(id);
        }

        public async Task<bool> IsCarBrandTitleExisted(string title, int? id = null)
        {
            return await _context.CarBrands.AnyAsync(c=> c.Title == title && c.Id!= id);
        }

        public async Task<bool> IsCarBrandSlugExisted(string slug, int? id = null)
        {
            return await _context.CarBrands.AnyAsync(c => c.Slug == slug && c.Id != id);
        }

        public async Task AddCarBrandAsync(CarBrand carBrand)
        {
            await _context.CarBrands.AddAsync(carBrand);
        }

        public void UpdateCarBrand(CarBrand carBrand)
        {
            _context.CarBrands.Update(carBrand);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
