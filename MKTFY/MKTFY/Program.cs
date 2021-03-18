using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MKTFY.App;
using MKTFY.App.Repositories;
using MKTFY.App.Repositories.Interfaces;
using MKTFY.App.Seeds;
using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKTFY
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // This is to separate the 'CreateHostBuilder(args).Build().Run()' into two separate steps in 
            // order to do the database migration part to create the db schema on the db server.
            
            var host = CreateHostBuilder(args).Build();

            using(var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    // This is to add the UserManager and RoleManager to the program startup so that it gets applied
                    // when the application gets loaded. Alternatively, this could be done through DB Migration but more complicated
                    var userManager = services.GetRequiredService<UserManager<User>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    var context = services.GetRequiredService<ApplicationDbContext>();

                    // This is to add the CategoryRepo to this variable so it can be passed in to the CategorySeeder below.
                    var categoryRepository = services.GetRequiredService<ICategoryRepository>();

                    // This is to run the DB migration at program start to update the DB with the latest migration files
                    context.Database.Migrate();

                    // This async function is required to add the seeder users and roles at the program start
                    Task.Run(async () => await UserAndRoleSeeder.SeedUsersAndRoles(roleManager, userManager)).Wait();

                    // This async function is required to add the seeder the Categories at the program start
                    Task.Run(async () => await CategorySeeder.AddDefaultCategories(categoryRepository)).Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occured while migrating database...");
                }
            }
            // This is the second and last step to get the app started.
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
