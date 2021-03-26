using IdentityModel.Client;
using MKTFY.Models.Entities;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Text;

namespace MKTFY.Models.ViewModels
{
    /// <summary>
    /// Forget Password Response View Model
    /// </summary>
    public class ForgetPwResponseVM
    {        
        /// <summary>
        /// ForgetPwResponseVM Constructor which takes in a Response object, resetToken and User data
        /// </summary>
        /// <param name="response"></param>
        /// <param name="resetToken"></param>
        /// <param name="user"></param>        
        public ForgetPwResponseVM(Response response, string resetToken, User user)
        {
            StatusCode = response.StatusCode.ToString();
            MessageBody = response.Body.ToString();
            ResetToken = resetToken;
            Email = user.Email;
        }
        /// <summary>
        /// ResetToken Field
        /// </summary>
        public string ResetToken { get; set; }
        /// <summary>
        /// Email Field
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// StatusCode Field
        /// </summary>
        public string StatusCode { get; set; }
        /// <summary>
        /// MessageBody Field
        /// </summary>
        public string MessageBody { get; set; }
    }
}
