using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MKTFY.Models.ViewModels
{
    public class ListingAddVM
    {
        [Required]
        public string Address { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
