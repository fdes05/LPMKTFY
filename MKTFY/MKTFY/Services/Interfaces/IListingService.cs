using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKTFY.Services.Interfaces
{
    public interface IListingService
    {
        public Task<Listing> EditListing(Guid id, Listing data);
        public Task<List<Listing>> GetListingsByCategory(Guid id, string userId, string searchTerm);
        public Task<List<Listing>> GetDealsWithLastThreeSearches(string searchTerm, string userId);
    }
}
