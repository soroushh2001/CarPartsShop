using CarPartsShop.Domain.Entities.Account;

namespace CarPartsShop.Application.ViewModels.Accounts
{
    public class UserViewModel
    {
        public string Email { get; set; }
        public DateTime RegisteredDate { get; set; }
        public List<string> Roles { get; set; }

        public bool IsLockedOut { get; set; }
    }
}
