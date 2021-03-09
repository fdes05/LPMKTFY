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
    public interface IUserService
    {
        public Task<User> GetUser(string email);

        public Task<IdentityResult> RegisterUser(RegisterVM data);

        public Task<TokenResponse> GetAccessToken(LoginVM login);

        public Task<bool> VerifyEmail(string userEmail);
    }
}
