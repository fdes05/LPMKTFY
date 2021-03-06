using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MKTFY.Models.ViewModels
{
    public class ListingAddVM
    {
        [Required]
        public string ProductName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Condition { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Location { get; set; }
    }
}
