using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MKTFY.Models.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            Created = DateTime.UtcNow;
        }
        [Key]
        public Guid Id { get; set; }
        [Required]
        public DateTime Created { get; set; }
    }
}
