using CarPartsShop.Application.Extensions;
using CarPartsShop.Application.Services.Interfaces;
using CarPartsShop.Application.Statics;
using CarPartsShop.Application.ViewModels.Sliders;
using CarPartsShop.Domain.Entities.Site;
using CarPartsShop.Domain.Interfaces;

namespace CarPartsShop.Application.Services.Implementations
{
    public class SliderService : ISliderService
    {
        private readonly ISliderRepository _sliderRepository;

        public SliderService(ISliderRepository sliderRepository)
        {
            _sliderRepository = sliderRepository;
        }

        public async Task<List<SliderViewModel>> GetAllSlidersAsync()
        {
            var sliders = await _sliderRepository.GetAllSlidersAsync();
            return sliders.Select(x => new SliderViewModel
            {
                ImageName = x.ImageName,
                Id = x.Id,
                Priority = x.Priority
            }).ToList();
        }

        public async Task<bool> CreateSliderAsync(CreateSliderViewModel create)
        {
            var imageName = create.Image.FileNameGenerator();

            var upResult = await create.Image.UploadImage(imageName, StaticDetails.SliderOrgServerPath,
                StaticDetails.SliderThumbServerPath);

            if (!upResult)
                return false;

            var slider = new Slider
            {
                ImageName = imageName,
                Priority = create.Priority
            };
            await _sliderRepository.AddSliderAsync(slider);
            await _sliderRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveSlider(int sliderId)
        {
            var slider = await _sliderRepository.GetSliderByIdAsync(sliderId);
            
            if(slider ==  null) return false;

            slider.ImageName.DeleteImage(StaticDetails.SliderOrgServerPath, StaticDetails.SliderThumbServerPath);

            _sliderRepository.RemoveSlider(slider);
            await _sliderRepository.SaveChangesAsync();
            return true;
        }
    }
}
