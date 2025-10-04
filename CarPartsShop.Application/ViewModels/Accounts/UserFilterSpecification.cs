using CarPartsShop.Application.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPartsShop.Application.ViewModels.Accounts
{
    public class UserFilterSpecification : BaseQueryParams
    {
        public string? Email { get; set; }

        public string? Role { get; set; }
    }
}
