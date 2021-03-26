using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MKTFY.Models.ViewModels
{
    /// <summary>
    /// Login View Model
    /// </summary>
    public class LoginVM
    {
        /// <summary>
        /// LoginVM Constructor
        /// </summary>
        public LoginVM() { }
        /// <summary>
        /// Additional LoginVM Constructor which takes in emailAddress, password, clientId
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="clientId"></param>
        public LoginVM(string email, string password, string clientId)
        {
            Email = email;
            Password = password;
            ClientId = clientId;
        }
        /// <summary>
        /// Email Field
        /// </summary>
        [Required]
        public string Email { get; set; }
        /// <summary>
        /// Password Field
        /// </summary>
        [Required]
        public string Password { get; set; }

        // This ClientId helps when handling logins from different front-ends (mobile or web) or when using third party
        // authentication services like Facebook, Google, Microsoft, etc.
        /// <summary>
        /// ClientId Field
        /// </summary>
        [Required]
        public string ClientId { get; set; }
    }
}
