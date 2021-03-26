using System;
using System.Collections.Generic;
using System.Text;

namespace MKTFY.Models.ViewModels
{
    /// <summary>
    /// Reset Password View Model
    /// </summary>
    public class ResetPwVM
    {
        /// <summary>
        /// ResetToken Field
        /// </summary>
        public string ResetToken { get; set; }
        /// <summary>
        /// Password Field
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// User email Field
        /// </summary>
        public string Email { get; set; }
    }
}
