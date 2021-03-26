using System;
using System.Collections.Generic;
using System.Text;

namespace MKTFY.Models.ViewModels
{
    /// <summary>
    /// Reset Password Response View Model
    /// </summary>
    public class ResetPwResponseVM
    {
        /// <summary>
        /// ResetPwResponseVM Constructor which takes in an email address and message
        /// </summary>
        /// <param name="email"></param>
        /// <param name="message"></param>
        public ResetPwResponseVM(string email, string message)
        {
            Email = email;
            Message = message;
        }
        /// <summary>
        /// Email Field
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Message Field
        /// </summary>
        public string Message { get; set; }
    }
}
