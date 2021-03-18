using System;
using System.Collections.Generic;
using System.Text;

namespace MKTFY.Models.Entities
{
    public class Search : BaseEntity<Guid>
    {
        public Search() : base() { }
        public Search(string userId, Guid categoryId, string searchTerm) : base()
        {
            UserId = userId;
            CategoryId = categoryId;
            SearchTerm = searchTerm;
        }
        public string UserId { get; set; }

        public Guid CategoryId { get; set; }

        public string SearchTerm { get; set; }
    }
}
