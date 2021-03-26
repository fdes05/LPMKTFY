using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MKTFY.Models.ViewModels
{
    /// <summary>
    /// User View Model
    /// </summary>
    public class UserVM 
    {
        /// <summary>
        /// UserVM Constructor which takes in a User Entity with user data
        /// </summary>
        /// <param name="src"></param>
        public UserVM(User src)
        {
            Id = src.Id;
            Email = src.Email;
            FirstName = src.FirstName;
            LastName = src.LastName;
        }
        /// <summary>
        /// UserId Field
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Email field
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// First Name Field
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Last Name Field
        /// </summary>
        public string LastName { get; set; }
    }
}
