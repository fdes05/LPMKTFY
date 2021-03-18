using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MKTFY.App.Repositories
{
    public class FaqRepository : BaseRepository<Guid, ApplicationDbContext, Faq>, IFaqRepository
    {
        public FaqRepository(ApplicationDbContext dbContext)
            : base(dbContext) // the constructor is required to hand the dbContext to the BaseRepository
        {

        }
    }
}
