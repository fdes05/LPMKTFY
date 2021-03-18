using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MKTFY.App
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Listing> Listings { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Faq> Faqs { get; set; }

        public DbSet<Search> Searches { get; set; }
    }
}
