using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
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
    /// <summary>
    /// This is the Account controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<User> _signinManager;
        private readonly UserManager<User> _userManager;
        private readonly IMailService _mailService;
        private readonly IUserService _userService;
        private readonly IPaymentService _paymentService;

        /// <summary>
        /// This is the AccountController Constructor which takes in the SignInManager, UserManager (both from Identity Framework), mailService and userService
        /// </summary>
        /// <param name="signinManager"></param>
        /// <param name="userManager"></param>
        /// <param name="mailService"></param>
        /// <param name="userService"></param>
        /// <param name="paymentService"></param>
        public AccountController(SignInManager<User> signinManager, UserManager<User> userManager, IMailService mailService, IUserService userService, IPaymentService paymentService)
        {
            _signinManager = signinManager;
            _userManager = userManager;
            _mailService = mailService;
            _userService = userService;
            _paymentService = paymentService;
        }

        /// <summary>
        /// Login endpoint
        /// </summary>
        /// <param name="login">Needs a LoginVM with JSON data (userName, </param>
        /// <returns>Returns LoginResponseVM with the AccessToken, User data and expire info</returns>       
        /// <response code="200">User Login successful</response>        
        /// <response code="500">Server failure, unknown reason</response>
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

        /// <summary>
        /// Email verification endpoint
        /// </summary>
        /// <param name="email">Needs the email address provided as JSON data</param>
        /// <returns>Returns a EmailVerificationResponseVM and sends an email to user with SendGrid</returns>
        /// <response code="200">Verification email sent to user email</response>        
        /// <response code="500">Server failure, unknown reason</response>        
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

        /// <summary>
        /// Register Endpoint
        /// </summary>
        /// <param name="data">Requires a RegisterVM with email, firstName, lastName, phoneNumber, country, city, address, password, confirmPassword, clientId</param>
        /// <returns>Returns a LoginResponseVM with AccessToke, User data and expire info</returns>
        /// <response code="200">Register successful and user saved</response>        
        /// <response code="500">Server failure, unknown reason</response> 
        [HttpPost("register")]
        public async Task<ActionResult<LoginResponseVM>> Register([FromBody] RegisterVM data)
        {
            //Create Stripe User for payment options
            var stripeCustomer = await _paymentService.CreateStripeCustomer(data);
            if (stripeCustomer == null)
                throw new UserNotSavedException("Something went wrong with saving the Stripe customer! please try again");

            //Register User
            IdentityResult result = await _userService.RegisterUser(data, stripeCustomer.Id);
            
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

        /// <summary>
        /// Forget Password Endpoint
        /// </summary>
        /// <param name="forgetInfo">Requires a ForgetPwVM with just the email as JSON data </param>
        /// <returns>Returns a ForgetPwResponseVM with a response object, reset token and user data </returns>
        /// <response code="200">Email to reset password sent to user email with ResetToken</response>        
        /// <response code="500">Server failure, unknown reason</response> 
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

            return Ok(result);
        }

        /// <summary>
        /// Reset Password Endpoint
        /// </summary>
        /// <param name="resetInfo">Requires a ResetPwVM as JSON data with the ResetToken, new Password and user email</param>
        /// <returns>Returns a ResetPwResponseVM with the user email and a message as JSON data</returns>
        /// <response code="200">Password updated for user with the one provided</response>        
        /// <response code="500">Server failure, unknown reason</response> 
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
                return Ok(result);
            }
            return BadRequest(new ResetPwResponseVM(user.Email, "something went wrong. Please try again"));
        }

        /// <summary>
        /// ViewProfile Endpoint
        /// </summary>
        /// <param name="id">Requires the userId in the URL</param>
        /// <returns>Returns a ProfileVM as JSON data with firstName, lastName, phoneNumber, emergencyContact, emergencyContactPhone, country, city, address</returns>
        /// <response code="200">Provides profile info</response> 
        /// <response code="401">Not currently logged in</response>
        /// <response code="403">User does not have access to resource</response>
        /// <response code="500">Server failure, unknown reason</response> 
        [HttpGet("profile/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "member")]
        public async Task<ActionResult<ProfileVM>> ViewProfile([FromRoute] string id)
        {
            // get the user
            var user = await _userService.GetUserById(id);

            // return the user object as a ProfileVM object
            return Ok(new ProfileVM(user));
        }

        /// <summary>
        /// Edit Profile
        /// </summary>
        /// <param name="id">Requires userId in the URL</param>
        /// <param name="data">Requires ProfileVM as JSON data with updated firstName, lastName, phoneNumber, emergencyContact, emergencyContactPhone, country, city, address</param>
        /// <returns>Returns a ProfileVM as JSON data with  with updated firstName, lastName, phoneNumber, emergencyContact, emergencyContactPhone, country, city, address</returns>
        /// <response code="200">Provides updated profile info</response> 
        /// <response code="401">Not currently logged in</response>
        /// <response code="403">User does not have access to resource</response>
        /// <response code="500">Server failure, unknown reason</response> 
        [HttpPut("profile/edit/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "member")]
        public async Task<ActionResult<ProfileVM>> EditProfile([FromRoute] string id, [FromBody] ProfileVM data)
        {
            // get the user
            var updatedUser = await _userService.EditUserProfile(id, new User(data));

            // return the user object as a ProfileVM object
            return Ok(new ProfileVM(updatedUser));
        }

        /// <summary>
        /// MyListings Endpoint
        /// </summary>
        /// <param name="id">Requires userId in URL</param>
        /// <returns>Returns a list of ListingVM based on the listings created by this user </returns>
        /// <response code="200">Provides a list of listings from this user</response> 
        /// <response code="401">Not currently logged in</response>
        /// <response code="403">User does not have access to resource</response>
        /// <response code="500">Server failure, unknown reason</response> 
        [HttpGet("mylistings/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "member")]
        public async Task<ActionResult<ListingVM>> MyListings([FromRoute] string id )
        {
            // get the listings by UserId
            var results = await _userService.GetUserListings(id);

            if (results == null)            
                throw new NotFoundException("User does not seem to have any listings yet. Please create a listing.");
            
            // return each listing from this user as a ListingVM
            var models = results.Select(item => new ListingVM(item));
            return Ok(models);
        }

        //[HttpGet("mypayments/{id}")]
        //public async Task<ActionResult<PaymentVM>> MyPayments([FromRoute] Guid id)
        //{
        //    // get the listings by UserId
        //    var results = await _userService.GetUserPayments(userId);

        //    // return each listing from this user as a ListingVM
        //    var models = results.Select(item => new PaymentVM(item));
        //    return Ok(models);
        //}
    }
}
