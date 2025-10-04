using CarPartsShop.Domain.Entities.Account;
using CarPartsShop.Domain.Interfaces;
using CarPartsShop.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPartsShop.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CarPartsShopDbContext _context;
        public UserRepository(CarPartsShopDbContext context)
        {
            _context = context;
        }

        public IQueryable<ApplicationUser> GetAllUsers()
        {
            return _context.Users
.AsQueryable();
        }
    }
}
