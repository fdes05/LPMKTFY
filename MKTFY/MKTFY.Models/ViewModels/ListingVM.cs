using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MKTFY.Models.ViewModels
{
    /// <summary>
    /// Listing View Model
    /// </summary>
    public class ListingVM
    {
        /// <summary>
        /// ListignVM Constructor which takes in a Listing Entity
        /// </summary>
        /// <param name="src"></param>
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
        /// <summary>
        /// ListingId Field
        /// </summary>
        [Required]
        public Guid Id { get; set; }
        /// <summary>
        /// Product Name Field
        /// </summary>
        [Required]
        public string ProductName { get; set; }
        /// <summary>
        /// Description Field
        /// </summary>
        [Required]
        public string Description { get; set; }
        /// <summary>
        /// CategoryId Field
        /// </summary>
        [Required]
        public Guid CategoryId { get; set; }
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
    }
}
