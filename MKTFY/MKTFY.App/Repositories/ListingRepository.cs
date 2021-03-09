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
    public class ListingRepository : BaseRepository<Guid, ApplicationDbContext, Listing>, IListingRepository
    {
        public ListingRepository(ApplicationDbContext dbContext)
            : base(dbContext) // need to hand down the ApplicationDbContext through DI from the main class 
                      // (ListingRepository) to the the constructor in the BaseRepository or it won't work!
        {           
        }        
    }
}
