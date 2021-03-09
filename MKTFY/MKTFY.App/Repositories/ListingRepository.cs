using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MKTFY.App.Exceptions;
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

        public async Task<Listing> Create(Listing entity)
        {      
            // Add and save the entity
            _context.Listings.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Listing> Edit(Guid id, Listing data)
        {
            var currentListing = await this.Get(id);

            // update the existing Listing           
            {
                currentListing.ProductName = data.ProductName;
                currentListing.Description = data.Description;
                currentListing.Category = data.Category;
                currentListing.Condition = data.Condition;
                currentListing.Price = data.Price;
                currentListing.Location = data.Location;
            }

            // Update and save the entity
            _context.Listings.Update(currentListing);
            await _context.SaveChangesAsync();

            return currentListing;
        }

        public async Task<Listing> Get(Guid id)
        {
            // Get the entity           
            var result = await _context.Listings.FirstOrDefaultAsync(i => i.Id == id);

            if (result == null)    
                throw new NotFoundException("item with id: " + id + " not found");
            

            return result;
        }

        public async Task<List<Listing>> GetAll()
        {
            // Get the entities
            var results = await _context.Listings.ToListAsync();            

            return results;
        }

        public async Task Delete(Guid id)
        {
            // Get the entity           
            var result = await _context.Listings.FirstOrDefaultAsync(i => i.Id == id);

            if (result == null)
                throw new NotFoundException("item with id: " + id + " not found");

            _context.Listings.Remove(result);
            await _context.SaveChangesAsync();
        }
    }
}
