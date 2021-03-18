using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MKTFY.Models.ViewModels
{
    public class ListingAddVM
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        public string Condition { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Location { get; set; }
    }
}
