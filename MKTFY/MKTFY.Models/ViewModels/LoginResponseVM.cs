using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace MKTFY.Models.ViewModels
{
    public class LoginResponseVM
    {
        public LoginResponseVM(TokenResponse tokenResponse, UserVM user)
        {
            AccessToken = tokenResponse.AccessToken;
            Expires = tokenResponse.ExpiresIn;
            User = user;
        }

        // <summary>
        // JWT Token
        // </summary>
        public string AccessToken { get; set; }

        // <summary>
        // Number of seconds until the access token expires
        // </summary>
        public long Expires { get; set; }

        // <summary>
        // Additional User data
        // </summary>
        public UserVM User { get; set; }
    }
}
