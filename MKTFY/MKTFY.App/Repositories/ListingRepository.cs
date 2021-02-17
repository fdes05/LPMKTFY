using Microsoft.EntityFrameworkCore;
using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.Entities;
using MKTFY.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories
{
    public class ListingRepository : IListingRepository
    {
        private readonly ApplicationDbContext _context;

        public ListingRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<ListingVM> Create(ListingAddVM src)
        {
            // Create the new entity
            var entity = new Listing(src);

            // Add and save the entity
            _context.Listings.Add(entity);
            await _context.SaveChangesAsync();

            return new ListingVM(entity);
        }

        public async Task<ListingVM> Get(Guid id)
        {
            // Get the entity
            // search term not working though: 
            // var result = await from listing in _context.Listings where listing.id == id select listing;
            var result = await _context.Listings.FirstAsync(i => i.Id == id);

            return new ListingVM(result);
        }

        public async Task<List<ListingVM>> GetAll()
        {
            // Get the entities
            var results = await _context.Listings.ToListAsync();

            var models = new List<ListingVM>();
            foreach (var entity in results)
                models.Add(new ListingVM(entity));

            return models;
        }
    }
}
