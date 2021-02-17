using MKTFY.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories.Interfaces
{
    public interface IListingRepository
    {
        // This Interface is needed for dependency injections that we setup in the Startup class under 'Configuration'
        // and 'services.AddScoped<Interface, Repository>()'

        Task<ListingVM> Create(ListingAddVM src);

        Task<List<ListingVM>> GetAll();

        Task<ListingVM> Get(Guid Id);
    }
}
