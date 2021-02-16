using MKTFY.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MKTFY.Models.Entities
{
    public class Listing : BaseEntity
    {
        public Listing(ListingAddVM src)
        {
            Address = src.Address;
            Description = src.Description;
        }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Description { get; set; }
       
    }
}
