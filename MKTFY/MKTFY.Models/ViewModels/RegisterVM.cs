using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MKTFY.Models.ViewModels
{
    /// <summary>
    /// Register View Model
    /// </summary>
    public class RegisterVM
    {
        /// <summary>
        /// Email Field
        /// </summary>
        [Required]
        public string Email { get; set; }
        /// <summary>
        /// FirstName Field
        /// </summary>
        [Required]
        public string FirstName { get; set; }
        /// <summary>
        /// LastName Field
        /// </summary>
        [Required]
        public string LastName { get; set; }
        /// <summary>
        /// PhoneNumber Field
        /// </summary>
        [Required]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Country Field
        /// </summary>
        [Required]
        public string Country { get; set; }
        /// <summary>
        /// City Field
        /// </summary>
        [Required]
        public string City { get; set; }
        /// <summary>
        /// Address Field
        /// </summary>
        [Required]
        public string Address { get; set; }
        /// <summary>
        /// Password Field
        /// </summary>
        [Required]        
        public string Password { get; set; }
        /// <summary>
        /// ConfirmPassword Field
        /// </summary>
        [Required]
        public string ConfirmPassword { get; set; }
        /// <summary>
        /// ClientId Field
        /// </summary>
        [Required]
        public string ClientId { get; set; }

    }
}
