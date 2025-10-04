using Microsoft.AspNetCore.Identity;

namespace CarPartsShop.Domain.Entities.Account
{
    public class ApplicationRole : IdentityRole<int>
    {
        public string Title { get; set; }

        public ICollection<ApplicationUserRole> UserRoles { get; set; }

    }
}
