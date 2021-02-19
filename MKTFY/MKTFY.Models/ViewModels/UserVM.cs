using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MKTFY.Models.ViewModels
{
    public class UserVM 
    {
        public UserVM(User src)
        {
            Id = src.Id;
            Email = src.Email;
            FirstName = src.FirstName;
            LastName = src.LastName;
        }
        public string Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
