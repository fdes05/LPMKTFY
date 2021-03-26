using Microsoft.EntityFrameworkCore;
using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories
{
    public class CategoryRepository : BaseRepository<Guid, ApplicationDbContext, Category>, ICategoryRepository
    {       
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {          
        }

        public async Task AddCategory(string Category)
        {
            var category = new Category(Category);
            _entityDbSet.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task<Category> FindCategoryByName(string categoryName)
        {
            var result = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryName == categoryName);            

            return result;
        }
    }
}
