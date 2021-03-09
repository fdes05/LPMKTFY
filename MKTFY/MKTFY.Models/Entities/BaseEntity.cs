using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MKTFY.Models.Entities
{
    public class BaseEntity<TId>
    {
        public BaseEntity()
        {
            Created = DateTime.UtcNow;
        }
        [Key]
        public TId Id { get; set; }
        [Required]
        public DateTime Created { get; set; }
    }
}
