using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using MKTFY.Models.Entities;
using MKTFY.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKTFY.Services.Interfaces
{
    /// <summary>
    /// Interface User Service
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Get User by Email (requires email address)
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Task<User> GetUserByEmail(string email);
        /// <summary>
        /// Get User by UserId (requires UserId)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<User> GetUserById(string id);
        /// <summary>
        /// Register User (requires RegisterVM with info)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Task<IdentityResult> RegisterUser(RegisterVM data);
        /// <summary>
        /// Get Access Token (requires LoginVM info)
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public Task<TokenResponse> GetAccessToken(LoginVM login);
        /// <summary>
        /// Verify Email (requires User email to verify email doesn't exist in DB already)
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        public Task<bool> VerifyEmail(string userEmail);
        /// <summary>
        /// Edit User Profile (requires UserId and an User Entity with updated info)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Task<User> EditUserProfile(string id, User data);
        /// <summary>
        /// Get User Listings (requires UserId and will return all Listings from this user)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<List<Listing>> GetUserListings(string userId);
    }
}
