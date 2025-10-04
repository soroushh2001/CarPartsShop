using CarPartsShop.Domain.Entities.Account;
using CarPartsShop.Domain.Entities.Common;

namespace CarPartsShop.Domain.Entities.Shop
{
    public class Comment : BaseEntity
    {
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
