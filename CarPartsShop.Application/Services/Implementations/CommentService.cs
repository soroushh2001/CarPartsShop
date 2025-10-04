using CarPartsShop.Application.Services.Interfaces;
using CarPartsShop.Application.ViewModels.Comments;
using CarPartsShop.Domain.Entities.Shop;
using CarPartsShop.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPartsShop.Application.Services.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task CreateComment(CreateCommentViewModel create, int userId)
        {
            var comment = new Comment
            {
                Message = create.Message,
                UserId = userId,
                ProductId = create.ProductId,
            };

            await _commentRepository.AddCommentAsync(comment);
            await _commentRepository.SaveChangesAsync();
        }

        public async Task<List<CommentViewModel>> GetAllProductCommentsAsync(int productId)
        {
            var query = _commentRepository.GetAllProductComments(productId);
            return await query.Select(x => new CommentViewModel
            {
                CreatedDate = x.CreatedDate,
                Message = x.Message,
                ProductName = x.Product.Title,
                UserEmail = x.User.Email
            }).ToListAsync();
        }
    }
}
