using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MKTFY.Models.ViewModels
{
    /// <summary>
    /// Listing Add View Model
    /// </summary>
    public class ListingAddVM
    {
        /// <summary>
        /// UserId Field
        /// </summary>
        [Required]
        public string UserId { get; set; }
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
