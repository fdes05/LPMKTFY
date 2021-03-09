using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MKTFY.App;
using MKTFY.App.Repositories.Interfaces;
using MKTFY.App.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MKTFY.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MKTFY.Middleware;
using MKTFY.Services.Interfaces;
using MKTFY.Services;

namespace MKTFY
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }               

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // This is to add the DbContext service that uses the 'DefaultConnection' string for the database
            // The 'DefaultConnection' needs to be added to the 'appsetting.json' as well as our Docker compsoe
            // won't be up to use the enviroment variable we've defined
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"),
                b =>
                {
                    b.MigrationsAssembly("MKTFY.App");
                })
            );

            // Add Identity Framework
            services.AddIdentity<User, IdentityRole>(options =>
            {
                // Add password requirements for users that register
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>() // This adds the AppDbContext so the Identities get saved in our DB
                .AddDefaultTokenProviders(); // In the test environment this is fine but in production it needs a verified Token provider 
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {   // This is the same Authority we set in the docker compose file as `http://mktfy.auth`.
                    options.Authority = Configuration.GetSection("Identity").GetValue<string>("Authority");
                    //Name of the API Resource
                    options.ApiName = "mktfyapi";
                    options.RequireHttpsMetadata = false;
                });

            services.AddControllers();

            // This is to add Dependency Injection for the ListingRepository. This is why we need the Interface IListingRepository
            services.AddScoped<IListingRepository, ListingRepository>();
            // This will add the Dependency Injection for the UserRepository.
            services.AddScoped<IUserRepository, UserRepository>();
            // This will add DI for MailService
            services.AddScoped<IMailService, MailService>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IListingService, ListingService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

           // app.UseHttpsRedirection();

            app.UseRouting();

            // Global error handler
            app.UseMiddleware<GlobalExceptionHandler>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
