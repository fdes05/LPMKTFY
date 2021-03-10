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
        private readonly UserManager<User> _userManager;
        private readonly IMailService _mailService;
        private readonly IUserService _userService;

        public AccountController(SignInManager<User> signinManager, UserManager<User> userManager, IMailService mailService, IUserService userService)
        {
            _signinManager = signinManager;                      
            _userManager = userManager;
            _mailService = mailService;
            _userService = userService;
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
            
            // Get user and generate userVM as LoginResponseVM further down requires token plus userVM
            var user = await _userService.GetUserByEmail(login.Email);           
            var tokenResponse = await _userService.GetAccessToken(login);

            if (tokenResponse.IsError)
            {
                return BadRequest(tokenResponse.Error);                
            }
            return Ok(new LoginResponseVM(tokenResponse, new UserVM(user)));
        }


        [HttpPost("register/emailverification")]
        public async Task<EmailVerificationResponseVM> RegisterEmailVerification([FromBody] EmailVerificationVM email)
        {
            // verify the email provided by the user does not already exist in DB
            var userEmail = email.Email;
            var userExists = await _userService.VerifyEmail(userEmail).ConfigureAwait(false);

            // Send email to user if email address does not exist in DB
            if (userExists == false)
            {
                var to = email.Email;
                var from = "fabio.destefani@launchpadbyvog.com";
                var subject = "Email verification link";
                var plainTextContent = "Please click on the link below to continue to the registration page.";
                var htmlContent = $"<a href=http:www.mktfy.com/regisration/?email=`{to}`>Link to registration page</a>";
                var response = await _mailService.SendEmail(to, from, subject, plainTextContent, htmlContent);
                return new EmailVerificationResponseVM(response);
            }
            throw new EmailVerificationException("Email already exists or is not valid. Use another email address.");
        }


        [HttpPost("register")]
        public async Task<ActionResult<LoginResponseVM>> Register([FromBody] RegisterVM data)
        {
            // FOR TESTING TO REMOVE DUPLICATE USER! REMOVE WHEN DONE
            var userDelete = await _userService.GetUserByEmail(data.Email);
            if (userDelete != null)
            {
                await _userManager.DeleteAsync(userDelete);
            }

            //Register User
            IdentityResult result = await _userService.RegisterUser(data);

            // Get the user profile and generate userVM for LoginResponseVM (requires token plus userVM)
            var user = await _userService.GetUserByEmail(data.Email);
            var userVM = new UserVM(user);

            // Get a LoginVM object to pass to the GetAccessToken userService
            LoginVM loginData = new LoginVM(data.Email, data.Password, data.ClientId);

            // Get a token from the identity server
            var tokenResponse = await _userService.GetAccessToken(loginData);

            if (tokenResponse.IsError)
            {
                return BadRequest(tokenResponse.Error);
                //return BadRequest("Unable to grant access to user account");
            }

            return Ok(new LoginResponseVM(tokenResponse, userVM));
        }


        [HttpPost("forgetPassword")]
        public async Task<ActionResult<ForgetPwResponseVM>> ForgetPassword([FromBody] ForgetPwVM forgetInfo)
        {
            // Get username/email and user object
            var username = forgetInfo.Email.ToLower();
            var user = await _userService.GetUserByEmail(forgetInfo.Email);

            // Generate password reset token from userManager
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user).ConfigureAwait(false);
            var encodedEmail = HttpUtility.UrlEncode(username); // making sure special caracters are not misinterpeted
            var encodedToken = Uri.EscapeDataString(resetToken);

            // Send email to user with password reset token and link to reset password
            var to = forgetInfo.Email;
            var from = "fabio.destefani@launchpadbyvog.com";
            var subject = "Password Reset Link";
            var plainTextContent = "Please click on the link below to continue to the password reset page.";
            var htmlContent = $"<a href=http:www.mktfy.com/passwordReset/?email=`{encodedEmail}` & `{encodedToken}` >Link to reset password page</a>";
            var response = await _mailService.SendEmail(to, from, subject, plainTextContent, htmlContent);

            var result = new ForgetPwResponseVM(response, resetToken, user);

            return result;
        }


        [HttpPost("resetPassword")]
        public async Task<ActionResult<ResetPwResponseVM>> ResetPassword([FromBody] ResetPwVM resetInfo)
        {
            // Get username/email and user object
            var username = resetInfo.Email.ToLower();
            var user = await _userService.GetUserByEmail(resetInfo.Email);

            // Reset password with userManager using the reset token generated in 'forgetPassword()' and 
            // using the new provided password from the front-end
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


        [HttpGet("profile/{id}")]
        public async Task<ProfileVM> ViewProfile([FromRoute] string id)
        {
            // get the user
            var user = await _userService.GetUserById(id);

            // return the user object as a ProfileVM object
            return new ProfileVM(user);
        }


        [HttpPut("profile/edit/{id}")]
        public async Task<ProfileVM> EditProfile([FromRoute] string id, [FromBody] ProfileVM data)
        {
            // get the user
            var updatedUser = await _userService.EditUserProfile(id, new User(data));

            // return the user object as a ProfileVM object
            return new ProfileVM(updatedUser);
        }
    }
}
