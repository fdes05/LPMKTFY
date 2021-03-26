using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MKTFY.Models.Entities
{
    /// <summary>
    /// Base Entity
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    public class BaseEntity<TId>
    {
        /// <summary>
        /// Base Entity constructor
        /// </summary>
        public BaseEntity()
        {
            Created = DateTime.UtcNow;
        }

        /// <summary>
        /// Base entity Id field
        /// </summary>
        [Key]
        public TId Id { get; set; }

        /// <summary>
        /// Base Entity DateTime field
        /// </summary>
        [Required]
        public DateTime Created { get; set; }
    }
}
