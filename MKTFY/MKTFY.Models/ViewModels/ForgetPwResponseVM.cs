using IdentityModel.Client;
using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MKTFY.Models.ViewModels
{
    public class ForgetPwResponseVM
    {
         public ForgetPwResponseVM(string resetToken, User user)
        {
            ResetToken = resetToken;
            Email = user.Email;
        }

        public string ResetToken { get; set; }

        public string Email { get; set; }
    }
}
