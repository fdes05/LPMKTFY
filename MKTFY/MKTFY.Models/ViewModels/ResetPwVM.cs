using System;
using System.Collections.Generic;
using System.Text;

namespace MKTFY.Models.ViewModels
{
    public class ResetPwVM
    {
        public string ResetToken { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
    }
}
