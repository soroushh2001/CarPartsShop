using CarPartsShop.Domain.Entities.Site;
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
    public class SliderRepository : ISliderRepository
    {
        private readonly CarPartsShopDbContext _context;
        public SliderRepository(CarPartsShopDbContext context)
        {
            _context = context;
        }

        public void RemoveSlider(Slider slider)
        {
            _context.Remove(slider);
        }

        public async Task AddSliderAsync(Slider slider)
        {
            await _context.AddAsync(slider);
        }

        public async Task<List<Slider>> GetAllSlidersAsync()
        {
            return await _context.Sliders.OrderBy(x=> x.Priority).ThenByDescending(x=> x.CreatedDate).ToListAsync();
        }

        public async Task<Slider?> GetSliderByIdAsync(int id)
        {
            return await _context.Sliders.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
