using DiplomToyStore.Data;
using DiplomToyStore.Data.AbstractRepo;
using DiplomToyStore.Data.ConcreteRepo;
using DiplomToyStore.Helpers;
using DiplomToyStore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DiplomToyStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            //Add EntityFramework services
            services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                });

            services.AddTransient<EmailService>();
            //Add Identity services
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.SignIn.RequireConfirmedEmail = true;
                })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IProductRepository, EFProductRepository>();

            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler("/Error");
            app.UseStatusCodePagesWithReExecute("/Error/{0}");

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                       name: null,
                       pattern: "Products/{category}/Page{productPage:int}",
                       defaults: new { Controller = "Home", action = "Index" });

                endpoints.MapControllerRoute(
                      name: null,
                      pattern: "Products/Page{productPage:int}",
                      defaults: new
                      {
                          Controller = "Home",
                          action = "Index",
                          productPage = 1
                      });

                endpoints.MapControllerRoute(
                      name: null,
                      pattern: "Products/{category}",
                      defaults: new
                      {
                          Controller = "Home",
                          action = "Index",
                          productPage = 1
                      });

                endpoints.MapControllerRoute(
                      name: null,
                      pattern: "",
                      defaults: new
                      {
                          Controller = "Home",
                          action = "Index",
                          productPage = 1
                      });

                endpoints.MapDefaultControllerRoute();
            });

            using (var scope = app.ApplicationServices.CreateScope())
            {
                Seeder.SeedData(scope.ServiceProvider);
                Seeder.CreateAdminAccount(scope.ServiceProvider, Configuration).Wait();
            }
        }
    }
}
