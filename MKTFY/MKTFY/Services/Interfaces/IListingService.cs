using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKTFY.Services.Interfaces
{
    public interface IListingService
    {
        public Task<Listing> EditListing(Guid id, Listing data);
    }
}
