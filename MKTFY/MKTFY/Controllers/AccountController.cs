using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MKTFY.App.Exceptions;
using MKTFY.App.Repositories;
using MKTFY.App.Repositories.Interfaces;
using MKTFY.Middleware;
using MKTFY.Models.Entities;
using MKTFY.Models.ViewModels;
using MKTFY.Services.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace MKTFY.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<User> _signinManager;
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMailService _mailService;
        
        public AccountController(SignInManager<User> signinManager, UserManager<User> userManager, IConfiguration configuration, IUserRepository userRepository, IMailService mailService)
        {
            _signinManager = signinManager;
            _configuration = configuration;
            _userRepository = userRepository;
            _userManager = userManager;
            _mailService = mailService;
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

        [HttpPost("register/emailverification")]
        public async void RegisterEmailVerification([FromBody] EmailVerificationVM email)
        {
            var userEmail = email.Email;
            var userExists = await _userRepository.VerifyEmail(userEmail);
            if (userExists == false)
            {
                var to = email.Email;
                var from = "fabio.destefani@launchpad.com";
                var subject = "Email verification link";
                var plainTextContent = "Please click on the link below to continue to the registration page.";
                var htmlContent = $"<a href=http:www.mktfy.com/regisration/?email=`{to}`>Link to registration page</a>";
                var response = await _mailService.SendEmail(to, from, subject, plainTextContent, htmlContent);
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<LoginResponseVM>> Register([FromBody] RegisterVM registerData)
        {
            var user = new User();
            // Create user and add info from user registration
            user.UserName = registerData.Email;
            user.Email = registerData.Email;
            user.FirstName = registerData.FirstName;
            user.LastName = registerData.LastName;
            user.PhoneNumber = registerData.PhoneNumber;
            user.Country = registerData.Country;
            user.City = registerData.City;
            user.Address = registerData.Address;

            // FOR TESTING TO REMOVE DUPLICATE USER! REMOVE WHEN DONE
            var userDelete = await _userRepository.GetByUserName(registerData.Email);
            await _userManager.DeleteAsync(userDelete);
            // save user through User Manager
            IdentityResult result = await _userManager.CreateAsync(user, registerData.Password);
            if (result.Succeeded)
                await _userManager.AddToRoleAsync(user, "member");

            // Get the user profile
            var userReturn = await _userRepository.GetByEmail(registerData.Email);

            // Get a token from the identity server
            using (var httpClient = new HttpClient())
            {
                var authority = _configuration.GetSection("Identity").GetValue<string>("Authority");

                // Make the call to our identity server
                var tokenResponse = await httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
                {
                    Address = authority + "/connect/token",
                    UserName = registerData.Email,
                    Password = registerData.Password,
                    ClientId = registerData.ClientId,
                    ClientSecret = "UzKjRFnAHffxUFati8HMjSEzwMGgGhmn",
                    Scope = "mktfyapi.scope"

                }).ConfigureAwait(false);

                if (tokenResponse.IsError)
                {
                    return BadRequest(tokenResponse.Error);
                    //return BadRequest("Unable to grant access to user account");
                }

                return Ok(new LoginResponseVM(tokenResponse, userReturn));
            }
        }

        [HttpPost("forgetPassword")]
        public async Task<ActionResult<ForgetPwResponseVM>> ForgetPassword([FromBody] ForgetPwVM forgetInfo)
        {
            var username = forgetInfo.Email.ToLower();
            var user = await _userRepository.GetByUserName(username).ConfigureAwait(false);

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user).ConfigureAwait(false);
            var encodedEmail = HttpUtility.UrlEncode(username); // making sure special caracters are not misinterpeted
            var encodedToken = Uri.EscapeDataString(resetToken);

            var apiKey = _configuration.GetSection("SendGrid").GetValue<string>("Key");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("fabio.destefani@launchpadbyvog.com", "Admin");
            var subject = "Password Reset Link";
            var to = new EmailAddress(username, "Valued Customer");
            var plainTextContent = "Please click on the link below to continue to the password reset page.";
            var htmlContent = $"<a href=http:www.mktfy.com/passwordReset/?email=`{encodedEmail}` & `{encodedToken}` >Link to reset password page</a>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

            var result = new ForgetPwResponseVM(resetToken, user);

            return result;
        }

        [HttpPost("resetPassword")]
        public async Task<ActionResult<ResetPwResponseVM>> ResetPassword([FromBody] ResetPwVM resetInfo)
        {
            var username = resetInfo.Email.ToLower();
            var user = await _userRepository.GetByUserName(username).ConfigureAwait(false);

            var resetPasswordResult = await _userManager.ResetPasswordAsync(
                user, resetInfo.ResetToken, resetInfo.Password)
                .ConfigureAwait(false);

            //check for password validation errors
            if (resetPasswordResult.Errors.Any(e => e.Code.Contains("PasswordRequires")))
            {
                throw new PasswordValidationException("password must contain at least six characters with " +
                    "one upper and lower case letter plus at least one numeric.");
            }
            if (resetPasswordResult.Succeeded == true)
            {
                var result = new ResetPwResponseVM(user.Email, "Password reset was successful! Please login.");
                return result;
            }
            return new ResetPwResponseVM(user.Email, "something went wrong. Please try again");
        }
    }
}
