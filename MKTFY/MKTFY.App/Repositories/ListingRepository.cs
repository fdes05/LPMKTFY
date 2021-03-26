using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MKTFY.App.Exceptions;
using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.Entities;
using MKTFY.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace MKTFY.App.Repositories
{
    public class ListingRepository : BaseRepository<Guid, ApplicationDbContext, Listing>, IListingRepository
    {
        public ListingRepository(ApplicationDbContext dbContext)
            : base(dbContext) // need to hand down the ApplicationDbContext through DI from the main class 
                      // (ListingRepository) to the the constructor in the BaseRepository or it won't work!
        {           
        }

        public async Task<List<Listing>> GetListingsbyCategory(Guid id)
        {
            var listings =  await _context.Listings.Where(l => l.CategoryId == id).ToListAsync();
            var categories = await _context.Categories.Where(c => c.Id == id).ToListAsync();
            var category = categories[0];
            var listingsCat = category.Listing.ToList();

            return listings;
        }
               

        public async Task<Search> saveSearchTerm(Guid id, string userId, string searchTerm)
        {
            // create Search object to be stored
            var searchObject = new Search(userId, id, searchTerm);

            // add Search object to DB and save
            var result = await _context.Searches.AddAsync(searchObject);
            await _context.SaveChangesAsync();
                        
            // verify that there are only 3 Search entries in DB and delete oldest Search entry if more than 3
            if(_context.Searches.Where(s => s.UserId == userId).Count() > 3)
            {
                // find the oldest time of Search items by this UserId in the DB based on the 'Created' attribute
                var oldestTime = await _context.Searches.Where(s => s.UserId == userId).MinAsync(s => s.Created);
                // Find the 'Search' entity with that oldestTime from the step before
                // The return type of 'Where' is IQueryable so needs the 'ToListAsync()' to finish the connection or else the context connection is kept open and the forEach loop creates an error
                var oldestSearch = await _context.Searches.Where(s => s.Created == oldestTime).ToListAsync();
                                                
                foreach(var item in oldestSearch)
                {
                    _context.Searches.Remove(item);                    
                }
                await _context.SaveChangesAsync();
            }
            return result.Entity;
        }

        public async Task<List<Search>> GetUserSearches(string userId)
        {
            // Get the three or fewer Search objects for a specific user
            var listUserSearches = await _context.Searches.Where(s => s.UserId == userId).ToListAsync();

            return listUserSearches;
        }       
    }
}
