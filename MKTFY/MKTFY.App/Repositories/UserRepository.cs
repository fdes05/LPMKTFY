using Microsoft.AspNetCore.Mvc;
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

        public async Task<User> GetByEmail(string email)
        {
            // Get the entity
            var result = await _context.Users.FirstOrDefaultAsync(user => user.Email == email);

            return result;
        }

        public async Task<User> GetByUserName(string userName)
        {
            // Get the entity
            var result = await _context.Users.FirstOrDefaultAsync(user => user.UserName == userName);

            return result;
        }

        public async Task<User> VerifyEmail(string userEmail)
        {
            var result = await _context.Users.FirstOrDefaultAsync(user => user.Email == userEmail);

            return result;
        }
    }
}
