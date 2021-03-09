using IdentityModel.Client;
using MKTFY.Models.Entities;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Text;

namespace MKTFY.Models.ViewModels
{
    public class ForgetPwResponseVM
    {        
        public ForgetPwResponseVM(Response response, string resetToken, User user)
        {
            StatusCode = response.StatusCode.ToString();
            MessageBody = response.Body.ToString();
            ResetToken = resetToken;
            Email = user.Email;
        }

        public string ResetToken { get; set; }

        public string Email { get; set; }

        public string StatusCode { get; set; }

        public string MessageBody { get; set; }
    }
}
