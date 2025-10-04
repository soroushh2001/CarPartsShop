using CarPartsShop.Application.ViewModels.Sliders;

namespace CarPartsShop.Application.Services.Interfaces
{
    public interface ISliderService
    {
        Task<List<SliderViewModel>> GetAllSlidersAsync();
        Task<bool> CreateSliderAsync(CreateSliderViewModel create);
        Task<bool> RemoveSlider(int sliderId);
    }
}
