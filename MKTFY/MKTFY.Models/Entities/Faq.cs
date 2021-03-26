using MKTFY.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MKTFY.Models.Entities
{
    /// <summary>
    /// FAQ Entity
    /// </summary>
    public class Faq : BaseEntity<Guid> 
    {
        /// <summary>
        /// FAQ Constructor
        /// </summary>
        public Faq() : base() { }
        /// <summary>
        /// Additional FAQ Constructor which takes in a FaqAddVM object (title and description)
        /// </summary>
        /// <param name="faq"></param>
        public Faq(FaqAddVM faq) : base()
        {
            Title = faq.Title;
            Description = faq.Description;
        }

        /// <summary>
        /// Title Field
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Description Field
        /// </summary>
        [Required]
        public string Description { get; set; }
    }
}
