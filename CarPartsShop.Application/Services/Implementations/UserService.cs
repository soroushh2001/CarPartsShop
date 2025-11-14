using CarPartsShop.Application.Services.Interfaces;
using CarPartsShop.Application.ViewModels.Accounts;
using CarPartsShop.Application.ViewModels.Common;
using CarPartsShop.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarPartsShop.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<FilterUserViewModel> FilterUsersAsync(FilterUserViewModel filter)
        {

            var query = _userRepository.GetAllUsers(); 

            if (!string.IsNullOrEmpty(filter.Search))
            {
                query = query.Where(x => x.Email.ToLower().Contains(filter.Search.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.Role))
            {
                query = query.Where(x => x.UserRoles.Any(ur => ur.Role.Name == filter.Role));
            }

            switch (filter.Sort)
            {
                case SortUser.Newest:
                    query = query.OrderByDescending(x => x.RegisteredDate);
                    break;
                case SortUser.Oldest:
                    break;
            }

            var items = query
                    .Include(x => x.UserRoles)
                    .ThenInclude(ur => ur.Role)
                    .Select(x => new UserViewModel
                    {
                        Email = x.Email,
                        RegisteredDate = x.RegisteredDate,
                        Roles = x.UserRoles
                            .Select(ur => ur.Role.Title)
                            .ToList(),
                        IsLockedOut = x.LockoutEnd.HasValue && x.LockoutEnd > DateTime.UtcNow
                    });

            await filter.SetPaging(items);

            return filter;
        }
    }
}
