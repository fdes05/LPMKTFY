using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.Entities;
using MKTFY.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MKTFY.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<User> _signinManager;
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public AccountController(SignInManager<User> signinManager, IConfiguration configuration, IUserRepository userRepository)
        {
            _signinManager = signinManager;
            _configuration = configuration;
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseVM>> Login([FromBody] LoginVM login)
        {
            // Validate the user login
            var result = await _signinManager.PasswordSignInAsync(login.Email, login.Password, false, true).ConfigureAwait(false);

            if (result.IsLockedOut)
                return BadRequest("This user account has been locked out, please try again later");
            else if (!result.Succeeded)
                return BadRequest("Invalid username/password");

            // Get the user profile
            var user = await _userRepository.GetByEmail(login.Email);

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

                if (tokenResponse.IsError)
                {
                    return BadRequest(tokenResponse.Error);
                    //return BadRequest("Unable to grant access to user account");
                }

                return Ok(new LoginResponseVM(tokenResponse, user));
            }
        }
    }
}
