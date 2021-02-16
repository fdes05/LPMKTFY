using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MKTFY.Models.ViewModels
{
    public class ListingVM
    {
        public ListingVM(Listing src)
        {
            Id = src.Id;
            Address = src.Address;
            Description = src.Description;
        }
        public Guid Id { get; set; }
        public string Address { get; set; }       
        public string Description { get; set; }
    }
}
