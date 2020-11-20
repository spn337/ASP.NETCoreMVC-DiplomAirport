using DiplomToyStore.Domain;
using DiplomToyStore.Domain.AbstractRepo;
using DiplomToyStore.Domain.ConcreteRepo;
using DiplomToyStore.Helpers;
using DiplomToyStore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace DiplomToyStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


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

            services.Configure<DataProtectionTokenProviderOptions>(o => 
                o.TokenLifespan = TimeSpan.FromHours(1)
            );

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();

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

            string uploadsFolder = Path.Combine(
                        env.ContentRootPath,
                        Configuration["ImagesPath"]);

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            //співставляємо шляхи з нашими кастомними каталогами
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(uploadsFolder),
                RequestPath = new PathString("/images")
            });


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
