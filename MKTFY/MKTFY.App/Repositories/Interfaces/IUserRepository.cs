
using Microsoft.AspNetCore.Mvc;
using MKTFY.Models.Entities;
using MKTFY.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByEmail(string email);
        Task<User> GetById(string id);
        Task<User> GetByUserName(string userName);
        Task<User> VerifyEmail(string userEmail);
        Task<User> EditUserProfile(User data);
        Task<List<Listing>> GetUserListings(string userId);
    }
}
