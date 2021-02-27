using Microsoft.EntityFrameworkCore;
using MKTFY.App.Repositories.Interfaces;
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
            var result = await _context.Users.FirstAsync(user => user.Email == email);

            // Build our view model
            var model = new UserVM(result);

            return model;
        }
    }
}
