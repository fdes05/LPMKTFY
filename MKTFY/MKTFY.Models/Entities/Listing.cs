using MKTFY.Models.ViewModels;
using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MKTFY.Models.Entities
{
    public class Listing : BaseEntity<Guid>
    {
        public Listing() : base() { }
        public Listing(ListingAddVM src) : base()
        {
            UserId = src.UserId;
            ProductName = src.ProductName;
            Description = src.Description;
            CategoryId = src.CategoryId;
            Condition = src.Condition;
            Price = src.Price;
            Location = src.Location;
        }

        [Required]
        public string ProductName { get; set; }
        
        [Required]
        public string Description { get; set; }

        [Required]
        public string Condition { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Location { get; set; }

        // This is to create the One-To-Many relationship between Category (one) and Listing (many)
        public Guid CategoryId { get; set; } // This will include a foreign key (FK) that is not-nullable
        public Category Category { get; set; }

        // This is to create the One-To-Many relationship between User (one) and Listing (many)
        public string UserId { get; set; } 
        public User User { get; set; }

    }
}
