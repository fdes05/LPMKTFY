using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.Entities;
using MKTFY.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKTFY.Services
{
    /// <summary>
    /// Listing Service
    /// </summary>
    public class ListingService : IListingService
    {
        private readonly IListingRepository _listingRepository;
        /// <summary>
        /// Listing Service Constructor
        /// </summary>
        /// <param name="listingRepository"></param>
        public ListingService(IListingRepository listingRepository)
        {
            _listingRepository = listingRepository;
        }
        /// <summary>
        /// Edit Listing (requires ListingId and Listing Entity with new info)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<Listing> EditListing(Guid id, Listing data)
        {
            var currentListing = await _listingRepository.Get(id);

            // update the existing Listing           
            {
                currentListing.ProductName = data.ProductName;
                currentListing.Description = data.Description;
                currentListing.Category = data.Category;
                currentListing.Condition = data.Condition;
                currentListing.Price = data.Price;
                currentListing.Location = data.Location;
            }

            // Save the update listing to the DB
            var savedListing = await _listingRepository.Edit(currentListing);
            
            return savedListing;
        }

        /// <summary>
        /// Get Listings by Category (requires CategoryId, UserId and optional search term)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        public async Task<List<Listing>> GetListingsByCategory(Guid id, string userId, string searchTerm)
        {           
            // getting a list of Listings from the repo based on the CategoryId (as a Guid)
            var listingsList = await _listingRepository.GetListingsbyCategory(id);

            if (searchTerm != null)
            {
                // add searchTerm to searchTermRepo to keep track of user searches                
                await _listingRepository.saveSearchTerm(id, userId, searchTerm);

                // filter through the list of listings 
                var filteredList = listingsList.Where(listing =>
                {                    
                    return listing.ProductName.ToLower().Contains(searchTerm.ToLower());                    
                }).ToList();
                
                return filteredList;
            }
            else
            {
                return listingsList;
            }            
        }
        /// <summary>
        /// Get Deals with last three Searches from specific user (requires UserId and optional search term)
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<Listing>> GetDealsWithLastThreeSearches(string searchTerm, string userId)
        {
            // get all Search objects from the DB
            var searchList = await _listingRepository.GetUserSearches(userId);
            // create a main list with all the Listings based on up to three searchTerms from the user
            var listingsList = new List<Listing>();

            // loop over the Search items in searchList and add the resulting 
            foreach (var item in searchList)
            {
                var getListingsPerSearchTerm = await this.GetListingsByCategory(item.CategoryId, item.UserId, item.SearchTerm);
                listingsList.AddRange(getListingsPerSearchTerm);
            }

            if(searchTerm != null)
            {
                // filter through the list of listings 
                var filteredList = listingsList.Where(listing =>
                {
                    return listing.ProductName.ToLower().Contains(searchTerm.ToLower());
                }).ToList();

                return filteredList;
            }
            else
            {
                return listingsList;
            }            
        }
    }
}
