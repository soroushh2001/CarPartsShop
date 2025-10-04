using CarPartsShop.Application.Services.Interfaces;
using CarPartsShop.Application.Statics;
using CarPartsShop.Application.ViewModels.Sliders;
using CarPartsShop.Mvc.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarPartsShop.Mvc.Areas.Admin.Controllers
{
    [Authorize(Roles = $"{RoleConstants.Admin},{RoleConstants.Seller}")]
    public class SlidersController : AdminBaseController
    {
        #region Constructor

        private readonly ISliderService _sliderService;

        public SlidersController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        #endregion

        #region SlidersList

        public async Task<IActionResult> Index()
        {
            return View(await _sliderService.GetAllSlidersAsync());
        }

        #endregion

        #region AddSlider

        [HttpGet]
        public IActionResult LoadAddSliderModal()
        {
            return PartialView("_AddSlider");
        }

        [HttpPost]
        public async Task<IActionResult> SubmitAddSliderModal(CreateSliderViewModel create)
        {
            var result = await _sliderService.CreateSliderAsync(create);
            if (result)
            {
                return JsonHelper.JsonResponse(200, "عملیات با موفقیت انجام شد");
            }
            return JsonHelper.JsonResponse(400, "فرمت عکس وارد شده صحیح نمی باشد");

        }

        #endregion

        #region RemoveSlider

        [HttpGet]
        public async Task<IActionResult> RemoveSlider(int sliderId)
        {
            var result = await _sliderService.RemoveSlider(sliderId);

            if (result)
            {
                return JsonHelper.JsonResponse(200, "عملیات با موفقیت انجام شد");

            }

            return JsonHelper.JsonResponse(400, "عملیات با شکست مواجه شد");
        }

        #endregion

    }
}
