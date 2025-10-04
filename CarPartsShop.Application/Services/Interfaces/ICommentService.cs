using CarPartsShop.Application.ViewModels.Comments;

namespace CarPartsShop.Application.Services.Interfaces
{
    public interface ICommentService
    {
        Task CreateComment(CreateCommentViewModel create,int userId);
        Task<List<CommentViewModel>> GetAllProductCommentsAsync(int productId);
    }
}
