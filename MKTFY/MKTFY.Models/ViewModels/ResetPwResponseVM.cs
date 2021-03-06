using System;
using System.Collections.Generic;
using System.Text;

namespace MKTFY.Models.ViewModels
{
    public class ResetPwResponseVM
    {
        public ResetPwResponseVM(string email, string message)
        {
            Email = email;
            Message = message;
        }

        public string Email { get; set; }

        public string Message { get; set; }
    }
}
