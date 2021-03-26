using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MKTFY.App.Repositories.Interfaces;
using MKTFY.App.Exceptions;
using MKTFY.Models.Entities;
using MKTFY.Models.ViewModels;
using MKTFY.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MKTFY.Services
{
    /// <summary>
    /// User Service
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        /// <summary>
        /// User Service Constructor which takes in an IUserRepository, IConfiguration and UserManager)
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="config"></param>
        /// <param name="userManager"></param>
        public UserService(IUserRepository userRepository, IConfiguration config, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _configuration = config;
            _userManager = userManager;

        }
        /// <summary>
        /// Get User by Email which takes in User Email address
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<User> GetUserByEmail(string email)
        {
            // Get the user profile
            var user = await _userRepository.GetByEmail(email);
            return user;
        }
        /// <summary>
        /// Get User by UserId which takes in UserId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User> GetUserById(string id)
        {
            // Get the user profile
            var user = await _userRepository.GetById(id);
            return user;
        }
        /// <summary>
        /// Get Access Token which takes in a LoginVM with email, password and clientId
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async Task<TokenResponse> GetAccessToken(LoginVM login)
        {
            // Get a token from the identity server
            using (var httpClient = new HttpClient())
            {
                var authority = _configuration.GetSection("Identity").GetValue<string>("Authority");

                // Make the call to our identity server
                var tokenResponse = await httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
                {
                    Address = authority + "/connect/token",
                    UserName = login.Email,
                    Password = login.Password,
                    ClientId = login.ClientId,
                    ClientSecret = "UzKjRFnAHffxUFati8HMjSEzwMGgGhmn",
                    Scope = "mktfyapi.scope"

                }).ConfigureAwait(false);

                return tokenResponse;
            }
        }
        /// <summary>
        /// Register User which takes in a RegisterVM with user info
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<IdentityResult> RegisterUser(RegisterVM data)
        {
#pragma warning disable IDE0017 // Simplify object initialization
            var user = new User();
#pragma warning restore IDE0017 // Simplify object initialization
                               // Create user and add info from user registration
            user.UserName = data.Email;
            user.Email = data.Email;
            user.FirstName = data.FirstName;
            user.LastName = data.LastName;
            user.PhoneNumber = data.PhoneNumber;
            user.Country = data.Country;
            user.City = data.City;
            user.Address = data.Address;

            // save user through User Manager
            IdentityResult result = await _userManager.CreateAsync(user, data.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "member");
                return result;
            }
            else
            {
                throw new UserNotSavedException("Something went wrong and user wasn't saved. Please try again.");
            }
        }
        /// <summary>
        /// Verify Email which takes in User Email and verifies it does not already exist in DB
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        public async Task<bool> VerifyEmail(string userEmail)
        {
            var result = await _userRepository.VerifyEmail(userEmail);
            if (result == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// Edit User Profile which takes is UserId and updated User data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<User> EditUserProfile(string id, User data)
        {
            ////add id to updated user object
            //data.Id = id;
            //data.UserName = data.Email;

           
            var currentUser = await _userRepository.GetById(id);
                        
            currentUser.FirstName = data.FirstName;
            currentUser.LastName = data.LastName;
            currentUser.PhoneNumber = data.PhoneNumber;
            currentUser.EmergencyContact = data.EmergencyContact;
            currentUser.EmergencyContactPhone = data.EmergencyContactPhone;
            currentUser.Country = data.Country;
            currentUser.City = data.City;
            currentUser.Address = data.Address;

            //var updatedUser = await _userRepository.EditUser(data)

            //update the user profile through user manager            
            var result = await _userRepository.EditUserProfile(currentUser);

            //get the updated user object from the UserRepo
            if (result != null)
            {
                return result;
            }
            throw new UserNotSavedException("Something went wrong with updating the user information. Please try again.");
        }
        /// <summary>
        /// Get User Listings which takes in UserId and returns all Listings from this User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<Listing>> GetUserListings(string userId)
        {
            var userListings = await _userRepository.GetUserListings(userId);
            return userListings;
        }
    }
}
