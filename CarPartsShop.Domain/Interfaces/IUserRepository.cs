using CarPartsShop.Domain.Entities.Account;

namespace CarPartsShop.Domain.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<ApplicationUser> GetAllUsers();
    }
}
