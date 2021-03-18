using MKTFY.Models.Entities;
using MKTFY.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories.Interfaces
{
    public interface IListingRepository : IBaseRepository<Guid, Listing>
    {
        // This Interface is needed for dependency injections that we setup in the Startup class under 'Configuration'
        // and 'services.AddScoped<Interface, Repository>()'

        // All the methods that need to be implemented are inherited from the IBaseRepository interface
        
        Task<List<Listing>> GetListingsbyCategory(Guid id);
        Task<Search> saveSearchTerm(Guid id, string userId, string searchTerm);
        Task<List<Search>> GetUserSearches(string userId);        
    }
}
