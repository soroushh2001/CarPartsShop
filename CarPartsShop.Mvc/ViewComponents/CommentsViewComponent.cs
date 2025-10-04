using CarPartsShop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarPartsShop.Mvc.ViewComponents
{
    public class CommentsViewComponent : ViewComponent
    {
        private readonly ICommentService _commentService;

        public CommentsViewComponent(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int productId)
        {
            return View(await _commentService.GetAllProductCommentsAsync(productId));
        }
    }
}
