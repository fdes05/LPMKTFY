using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MKTFY.Models.ViewModels
{
    public class ProfileVM
    {
        public ProfileVM(User user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Phone = user.PhoneNumber;
            EmergencyContact = user.FirstName;
            EmergencyContactPhone = user.FirstName;
            Country = user.Country;
            City = user.City;
            Address = user.Address;
        }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string EmergencyContact { get; set; }

        public string EmergencyContactPhone { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Address { get; set; }
    }
}
