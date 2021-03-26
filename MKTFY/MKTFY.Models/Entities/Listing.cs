
using MKTFY.Models.ViewModels;
using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MKTFY.Models.Entities
{
    /// <summary>
    /// Listing Entity
    /// </summary>
    public class Listing : BaseEntity<Guid>
    {
        /// <summary>
        /// Listing constructor
        /// </summary>
        public Listing() : base() { }
        /// <summary>
        /// Aditional Listing constructor which takes in a ListingAddVM (userId, ProductName, Description, CategoryId, Condition, Price, Location)
        /// </summary>
        /// <param name="src"></param>
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

        /// <summary>
        /// ProductName Field
        /// </summary>
        [Required]
        public string ProductName { get; set; }

        /// <summary>
        /// Description Field
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Condition Field
        /// </summary>
        [Required]
        public string Condition { get; set; }

        /// <summary>
        /// Price Field
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// Location Field
        /// </summary>
        [Required]
        public string Location { get; set; }

        // This is to create the One-To-Many relationship between Category (one) and Listing (many)
        /// <summary>
        /// CategoryId Field to define the Many-To-One relationshipt with Category
        /// </summary>
        public Guid CategoryId { get; set; } // This will include a foreign key (FK) that is not-nullable
        /// <summary>
        /// Category Field to define the Many-To-One relationshipt with Category
        /// </summary>
        public Category Category { get; set; }

        // This is to create the One-To-Many relationship between User (one) and Listing (many)
        /// <summary>
        /// UserId Field to define the Many-To-One relationshipt with User
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// User Field to define the Many-To-One relationshipt with User
        /// </summary>
        public User User { get; set; }

    }
}
