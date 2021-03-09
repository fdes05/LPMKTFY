using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.Entities;
using MKTFY.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKTFY.Services
{
    public class ListingService : IListingService
    {
        private readonly IListingRepository _listingRepository;
        public ListingService(IListingRepository listingRepository)
        {
            _listingRepository = listingRepository;
        }
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

    }
}
