using CarPartsShop.Application.Services.Interfaces;
using CarPartsShop.Application.ViewModels.Accounts;
using CarPartsShop.Application.ViewModels.Common;
using CarPartsShop.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarPartsShop.Application.Services.Implementations
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<FilterUserViewModel> FilterUsersAsync(UserFilterSpecification specification)
        {

            var query = _userRepository.GetAllUsers(); // اینجا Include شده

            if (!string.IsNullOrEmpty(specification.Search))
            {
                query = query.Where(x => x.Email.ToLower().Contains(specification.Search.ToLower()));
            }

            if (!string.IsNullOrEmpty(specification.Role))
            {
                query = query.Where(x => x.UserRoles.Any(ur => ur.Role.Name == specification.Role));
            }

            switch (specification.OrderBy)
            {
                case "Newest":
                    query = query.OrderByDescending(x => x.RegisteredDate);
                    break;
                case "Oldest":
                    query = query.OrderBy(x => x.RegisteredDate);
                    break;
                default:
                    query = query.OrderByDescending(x => x.RegisteredDate);
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

            var paging = await PaginatedList<UserViewModel>.CreateAsync(items, specification.PageIndex, specification.PageSize);

            return new FilterUserViewModel
            {
                Specification = specification,
                Users = paging
            };
        }
    }
}
