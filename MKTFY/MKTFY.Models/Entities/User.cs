using Microsoft.AspNetCore.Identity;
using MKTFY.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MKTFY.Models.Entities
{
    /// <summary>
    /// User Entity
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        /// User Constructor
        /// </summary>
        public User() : base() { }
        /// <summary>
        /// Additional User Constructor which takes in a ProfileVM (firstName, lastName, phoneNumber, emergencyContact, EmergencyContactPhone, Country, City, Address)
        /// </summary>
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
        /// EmergencyContact Field
        /// </summary>   
        public string EmergencyContact { get; set; }
        /// <summary>
        /// EmergencyContactPhone Field
        /// </summary>
        public string EmergencyContactPhone { get; set; }
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
        /// Listings Field for the Many-To-One relationship with Listings
        /// </summary>       
        public ICollection<Listing> Listing { get; set; }
    }
}
