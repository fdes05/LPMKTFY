using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MKTFY.Models.ViewModels
{
    /// <summary>
    /// Forget Password View Model
    /// </summary>
    public class ForgetPwVM
    {
        /// <summary>
        /// Email Field
        /// </summary>
        [Required]
        public string Email { get; set; }
    }
}
