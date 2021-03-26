using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MKTFY.Models.ViewModels
{
    /// <summary>
    /// Email Verification View Model
    /// </summary>
    public class EmailVerificationVM
    {
        /// <summary>
        /// Email Field
        /// </summary>
        [Required]
        public string Email { get; set; }
    }
}
