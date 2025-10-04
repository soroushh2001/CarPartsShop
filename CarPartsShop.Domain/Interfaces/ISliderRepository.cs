using CarPartsShop.Domain.Entities.Site;

namespace CarPartsShop.Domain.Interfaces
{
    public interface ISliderRepository
    {
        void RemoveSlider(Slider slider);
        Task AddSliderAsync(Slider slider);
        Task<List<Slider>> GetAllSlidersAsync();
        Task<Slider?> GetSliderByIdAsync(int id);
        Task SaveChangesAsync();
    }
}
