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
using Microsoft.OpenApi.Models;
using System.IO;
using MKTFY.SwashBuckle;

namespace MKTFY
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configuration Field
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// Startup Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// Configure Services
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // This is to add CORS (Enable Cross-Origin-Requests) for web requests in the dev environment
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:3000/*",
                                                          "http://localhost:33000/*")
                                             .AllowAnyOrigin()                                             
                                             .AllowAnyMethod()
                                             .AllowAnyHeader();
                                  });
            });

            // This is to add the DbContext service that uses the 'DefaultConnection' string for the database
            // The 'DefaultConnection' needs to be added to the 'appsetting.json' as well as our Docker compose
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

            // This is to add OpenAPI (Swagger) to your projects. 'Swashbuckle.AspNetCore' NuGet Package needs to be
            // added to the main MKTFY api project and the MKTFY.Models project. Also, need to go to project settings and
            // add under 'build' the 'Output' section with 'XML Documentation File' (default file loc is okay)
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MKTFY API", Version = "v1" });

                c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer"
                });
                c.OperationFilter<AuthHeaderOperationFilter>();

                var apiPath = Path.Combine(System.AppContext.BaseDirectory, "MKTFY.xml");
                var modelsPath = Path.Combine(System.AppContext.BaseDirectory, "MKTFY.Models.xml");
                c.IncludeXmlComments(apiPath);
                c.IncludeXmlComments(modelsPath);
            });

            // This is to add Dependency Injection for the ListingRepository. This is why we need the Interface IListingRepository
            services.AddScoped<IListingRepository, ListingRepository>();

            // This will add the Dependency Injection for the UserRepository.
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddScoped<IFaqRepository, FaqRepository>();

            // This will add DI for MailService
            services.AddScoped<IMailService, MailService>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IListingService, ListingService>();

            services.AddScoped<IFaqService, FaqService>();

            services.AddScoped<IPaymentService, PaymentService>();          

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// Configure Method
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Global error handler
            app.UseMiddleware<GlobalExceptionHandler>();

            // app.UseHttpsRedirection();

            app.UseRouting();

            // this is to use CORS (enable cross-origin-requests) for web api requests in dev as enable above
            app.UseCors(MyAllowSpecificOrigins);

            // this is to add the swagger UI so it looks nicer and we can add swagger comments to our endpoints
            // UI can be found at 'localhost:33000/swagger/index.html'
            // the if-statement is there to make sure we do not enable Swagger in Production environment.
            if (!env.IsProduction())
            {
                // this is to add swagger to our app (localhost:33000/swagger/v1/swagger.json)
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MKTFY API V1");
                });
            }            

            

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
