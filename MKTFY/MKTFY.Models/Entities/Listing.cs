using MKTFY.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MKTFY.Models.Entities
{
    public class Listing : BaseEntity
    {
        public Listing() : base() { }
        public Listing(ListingAddVM src) : base()
        {            
            ProductName = src.ProductName;
            Description = src.Description;
            Category = src.Category;
            Condition = src.Condition;
            Price = src.Price;
            Location = src.Location;
        }

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
