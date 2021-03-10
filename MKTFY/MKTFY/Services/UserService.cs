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
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;

        public UserService(IUserRepository userRepository, IConfiguration config, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _configuration = config;
            _userManager = userManager;

        }

        public async Task<User> GetUserByEmail(string email)
        {
            // Get the user profile
            var user = await _userRepository.GetByEmail(email);
            return user;
        }

        public async Task<User> GetUserById(string id)
        {
            // Get the user profile
            var user = await _userRepository.GetById(id);
            return user;
        }

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

        public async Task<IdentityResult> RegisterUser(RegisterVM data)
        {
            var user = new User();
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

        
        public async Task<User> EditUserProfile(string id, User data)
        {
            //add id to updated user object
            data.Id = id;

            //update the user profile through user manager            
            var result = _userManager.UpdateAsync(data);  
            
            //get the updated user object from the UserRepo
            var updatedUser = await _userRepository.GetById(data.Id);

            return updatedUser;
        }
    }
}
