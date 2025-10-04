using CarPartsShop.Application.ViewModels.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPartsShop.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<FilterUserViewModel> FilterUsersAsync(UserFilterSpecification specification);
    }
}
