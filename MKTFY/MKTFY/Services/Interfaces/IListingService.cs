using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKTFY.Services.Interfaces
{
    /// <summary>
    /// Interface Listing Service
    /// </summary>
    public interface IListingService
    {
        /// <summary>
        /// Edit Listing Service
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Task<Listing> EditListing(Guid id, Listing data);
        /// <summary>
        /// Get Listings by Category (requires CategoryId)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        public Task<List<Listing>> GetListingsByCategory(Guid id, string userId, string searchTerm);
        /// <summary>
        /// Get Deals with last three searches by this user (requires UserId)
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<List<Listing>> GetDealsWithLastThreeSearches(string searchTerm, string userId);
    }
}
