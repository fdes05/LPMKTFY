using MKTFY.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MKTFY.Models.Entities
{
    public class Faq : BaseEntity<Guid> 
    {
        public Faq() : base() { }
        public Faq(FaqAddVM faq) : base()
        {
            Title = faq.Title;
            Description = faq.Description;
        }

        [Required]
        public string Title { get; set; }


        [Required]
        public string Description { get; set; }
    }
}
