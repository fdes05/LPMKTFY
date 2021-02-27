using IdentityServer4.AspNetIdentity;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MKTFY.App;
using MKTFY.Auth.Services;
using MKTFY.Models.Entities;

namespace MKTFY.Auth
{
    public class Startup
    {
        // This is the consturctor that we had to add in order to be able to access the Startup class further down and save all those properties
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>  //This is to add the DB connection so the identity server can store user info in our DB
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"),
               b =>
               {
                   b.MigrationsAssembly("MKTFY.App");
               }));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>(); // this is neede so the identity server knows where to store Users and Identity Roles

            services.AddIdentityServer(option =>
            {
                option.IssuerUri = Configuration.GetSection("Identity").GetValue<string>("Authority");
            })
                .AddOperationalStore(options => // Here we add the operational store with the connection string to our DB
                {
                options.ConfigureDbContext = builder => builder.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"),
                        npgSqlOptions =>
                        {
                            npgSqlOptions.MigrationsAssembly("MKTFY.App");
                        });
                })
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(Config.IdentityResources) // The Identity framework requires us to define the Identity Resourses and Api resouces
                .AddInMemoryApiResources(Config.ApiResources) // The 'Config' is a class we added manually to the MKTFY.Auth project
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryClients(Config.Clients)
                .AddAspNetIdentity<User>();

            services.AddScoped<IProfileService, ProfileService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseIdentityServer();
        }
    }
}
