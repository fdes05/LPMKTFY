using Microsoft.EntityFrameworkCore;
using MKTFY.App.Exceptions;
using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories
{
    // using the constructor to set the Generic Types below through the Repository (like Listing) to set
    // what TPrimaryKey (id), TDbContext (could be different DB's) and TEntity (Listing or others)
    public class BaseRepository<TPrimaryKey, TDbContext, TEntity> : IBaseRepository<TPrimaryKey, TEntity>
        where TEntity : BaseEntity<TPrimaryKey> // Important: Need to declare that the base entity is not just
        // any type but comes from Base Entity with the TPrimaryKey (id of the BaseEntity)
        // The BaseEntity has the Id (TPrimaryKey) and not the TEntity so that's why we need to define this with 'where TEntity : BaseEntity<TPrimaryKey>'
        where TDbContext : DbContext // Same here, we need to tell .Net what TDbContext type is based on the DbContext type and not just anything (or else we could hand in strings, int, etc which would not be good)
    {
        protected readonly TDbContext _context;
        protected DbSet<TEntity> _entityDbSet;

        public BaseRepository(TDbContext context)
        {
            _context = context;
            _entityDbSet = context.Set<TEntity>(); // this is important to 'Set' the TEntity type when this class gets instantiated (called by a Listing or other repo)
        }
        public async Task<TEntity> Create(TEntity src)
        {
            // Add and save the entity
            _entityDbSet.Add(src);
            await _context.SaveChangesAsync();

            return src;
        }

        public async Task<TEntity> Edit(TEntity data)
        {     
            // Update and save the entity
            _entityDbSet.Update(data);
            await _context.SaveChangesAsync();

            return data;
        }

        public async Task<TEntity> Get(TPrimaryKey id)
        {
            // Get the entity
            var result = await _entityDbSet.FindAsync(id);

            if (result == null)
                throw new NotFoundException("item with id: " + id + " not found");


            return result;
        }

        public async Task<List<TEntity>> GetAll()
        {
            // Get the entities
            var results = await _entityDbSet.ToListAsync();

            return results;
        }

        public async Task Delete(TPrimaryKey id)
        {
            // Get the entity           
            var result = await _entityDbSet.FindAsync(id);

            if (result == null)
                throw new NotFoundException("item with id: " + id + " not found");

            // Renive the entity
            _entityDbSet.Remove(result);
            await _context.SaveChangesAsync();
        }
    }
}
