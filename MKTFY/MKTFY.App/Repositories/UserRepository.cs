using Microsoft.EntityFrameworkCore;
using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.Entities;
using MKTFY.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<UserVM> GetByEmail(string email)
        {
            // Get the entity
            var result = await _context.Users.FirstOrDefaultAsync(user => user.Email == email);

            // Build our view model
            var model = new UserVM(result);

            return model;
        }

        public async Task<User> GetByUserName(string userName)
        {
            // Get the entity
            var result = await _context.Users.FirstOrDefaultAsync(user => user.UserName == userName);

            return result;
        }

        public async Task<bool> VerifyEmail(string email)
        {
            var result = await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
            if(result == null)
            {
                return false;
            }
            return true;
        }
    }
}
