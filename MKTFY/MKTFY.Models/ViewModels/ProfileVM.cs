using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MKTFY.Models.ViewModels
{
    public class ProfileVM
    {
        // The default constructor was overridden by the 'ProfileVM(User user)' but needs to be added back
        // in order for the HttpPut to work as it needs the default constructor.
        public ProfileVM() { }
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

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string EmergencyContact { get; set; }

        public string EmergencyContactPhone { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Address { get; set; }
    }
}
