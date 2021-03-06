using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MKTFY.Models.ViewModels
{
    public class ForgetPwVM
    {
        [Required]
        public string Email { get; set; }
    }
}
