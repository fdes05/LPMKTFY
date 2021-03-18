using Microsoft.AspNetCore.Identity;
using MKTFY.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MKTFY.Models.Entities
{
    public class User : IdentityUser
    {
        public User() : base() { }
        public User(ProfileVM user) : base()
        {            
            FirstName = user.FirstName;
            LastName = user.LastName;
            PhoneNumber = user.PhoneNumber;
            EmergencyContact = user.EmergencyContact;
            EmergencyContactPhone = user.EmergencyContactPhone;
            Country = user.Country;
            City = user.City;
            Address = user.Address;
        }

        
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
                
        public string EmergencyContact { get; set; }

        public string EmergencyContactPhone { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
