using CarPartsShop.Domain.Entities.Shop;
using CarPartsShop.Domain.Interfaces;
using CarPartsShop.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CarPartsShop.Infra.Data.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly CarPartsShopDbContext _context;
        public CommentRepository(CarPartsShopDbContext context)
        {
            _context = context;
        }

        public async Task AddCommentAsync(Comment comment)
        {
            await _context.AddAsync(comment);
        }

        public IQueryable<Comment> GetAllProductComments(int productId)
        {
            return _context.Comments.Include(x=> x.User).Include(x=> x.Product)
                .Where(x=> x.ProductId == productId)
                .AsQueryable();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
