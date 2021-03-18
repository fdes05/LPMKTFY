using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Guid, Category>
    {
        Task AddCategory(string Category);

        Task<Category> FindCategoryByName(string categoryName);
    }
}
