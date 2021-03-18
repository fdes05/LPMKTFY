using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MKTFY.Models.Entities
{
    public class Category : BaseEntity<Guid>
    {
        public Category() : base() { }
        public Category(string category) : base()
        {
            CategoryName = category;
        }

        [Required]
        public string CategoryName { get; set; }

        // This is to add the One-To-Many relationship between Listing (many) and Category (one)
        // Only needed if we wanted to search for Listings through querring a specific category and getting all
        // listings in that category. In our case we do it through listings and sort by category so not really needed here but I leave it as an example
        public ICollection<Listing> Listing { get; set; }
    }
}
