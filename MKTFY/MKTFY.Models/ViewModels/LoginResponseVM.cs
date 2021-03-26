using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace MKTFY.Models.ViewModels
{
    /// <summary>
    /// Login Response View Model
    /// </summary>
    public class LoginResponseVM
    {
        /// <summary>
        /// LoginResponseVM Constructor
        /// </summary>
        /// <param name="tokenResponse"></param>
        /// <param name="user"></param>
        public LoginResponseVM(TokenResponse tokenResponse, UserVM user)
        {
            AccessToken = tokenResponse.AccessToken;
            Expires = tokenResponse.ExpiresIn;
            User = user;
        }

        /// <summary>
        /// Access Token Field
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Expires Field
        /// </summary>
        public long Expires { get; set; }

        /// <summary>
        /// User Field
        /// </summary>
        public UserVM User { get; set; }
    }
}
