using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MKTFY.Models.ViewModels
{
    public class ListingEditVM : ListingAddVM
    {               
        [Required]
        public Guid Id { get; set; }                
    }
}
