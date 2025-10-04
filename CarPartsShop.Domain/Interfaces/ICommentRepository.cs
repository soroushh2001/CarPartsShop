using CarPartsShop.Domain.Entities.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPartsShop.Domain.Interfaces
{
    public interface ICommentRepository
    {
        IQueryable<Comment> GetAllProductComments(int productId);
        Task AddCommentAsync(Comment comment);
        Task SaveChangesAsync();
    }
}
