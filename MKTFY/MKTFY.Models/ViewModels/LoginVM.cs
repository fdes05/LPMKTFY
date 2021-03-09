using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MKTFY.Models.ViewModels
{
    public class LoginVM
    {
        public LoginVM() { }
        public LoginVM(string email, string password, string clientId)
        {
            Email = email;
            Password = password;
            ClientId = clientId;
        }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        // This ClientId helps when handling logins from different front-ends (mobile or web) or when using third party
        // authentication services like Facebook, Google, Microsoft, etc.
        [Required]
        public string ClientId { get; set; }
    }
}
