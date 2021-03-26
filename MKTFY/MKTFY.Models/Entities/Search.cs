using System;
using System.Collections.Generic;
using System.Text;

namespace MKTFY.Models.Entities
{
    /// <summary>
    /// Search Entity
    /// </summary>
    public class Search : BaseEntity<Guid>
    {
        /// <summary>
        /// Search Constructor
        /// </summary>
        public Search() : base() { }
        /// <summary>
        /// Search Constructor which takes in parameters
        /// </summary>
        /// <param name="userId">(string)userId</param>
        /// <param name="categoryId">(Guid)categoryId</param>
        /// <param name="searchTerm">(string)searchTerm</param>
        public Search(string userId, Guid categoryId, string searchTerm) : base()
        {
            UserId = userId;
            CategoryId = categoryId;
            SearchTerm = searchTerm;
        }
        /// <summary>
        /// UserId Field
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// CategoryId Field
        /// </summary>
        public Guid CategoryId { get; set; }
        /// <summary>
        /// SearchTerm Field
        /// </summary>
        public string SearchTerm { get; set; }
    }
}
