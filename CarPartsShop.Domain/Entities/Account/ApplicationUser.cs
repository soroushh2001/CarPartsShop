using CarPartsShop.Domain.Entities.Order;
using CarPartsShop.Domain.Entities.Shop;
using Microsoft.AspNetCore.Identity;

namespace CarPartsShop.Domain.Entities.Account
{
    public class ApplicationUser : IdentityUser<int>
    {
        public DateTime RegisteredDate { get; set; } = DateTime.Now;

        public ICollection<IdentityUserClaim<int>> Claims { get; set; }
        public ICollection<IdentityUserLogin<int>> Logins { get; set; }
        public ICollection<IdentityUserToken<int>> Tokens { get; set; }
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
        public ICollection<Order.Order> Orders { get; set; } 
        public ICollection<Comment> Comments { get; set; }  
    }
}
