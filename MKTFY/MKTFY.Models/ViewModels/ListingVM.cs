using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MKTFY.Models.ViewModels
{
    public class ListingVM
    {
        public ListingVM(Listing src)
        {
            Id = src.Id;
            ProductName = src.ProductName;
            Description = src.Description;
            CategoryId = src.CategoryId;
            Condition = src.Condition;
            Price = src.Price;
            Location = src.Location;            
        }

        [Required]
        public Guid Id { get; set; }

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
