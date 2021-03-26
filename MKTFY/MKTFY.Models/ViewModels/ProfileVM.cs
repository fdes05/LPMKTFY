using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MKTFY.Models.ViewModels
{
    /// <summary>
    /// Profile View Model
    /// </summary>
    public class ProfileVM
    {
        // The default constructor was overridden by the 'ProfileVM(User user)' but needs to be added back
        // in order for the HttpPut to work as it needs the default constructor.
        /// <summary>
        /// ProfileVM Constructor
        /// </summary>
        public ProfileVM() { }
        /// <summary>
        /// Additional ProfileVM Constructor which takes in a User object (firstName, lastName, phoneNumber, emergencyContact, emergencyContactPhone, country, city, address)
        /// </summary>
        /// <param name="user"></param>
        public ProfileVM(User user)
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
        public string FirstName { get; set; }
        /// <summary>
        /// LastName Field
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// PhoneNumber Field
        /// </summary>
        public string PhoneNumber { get; set; }
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
        public string Country { get; set; }
        /// <summary>
        /// City Field
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Address Field
        /// </summary>
        public string Address { get; set; }
    }
}
