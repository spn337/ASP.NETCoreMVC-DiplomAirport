﻿using DiplomAirport.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomAirport.Data
{
    public static class Seeder
    {
        public static async Task CreateAdminAccount(
            IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var user = new ApplicationUser
            {
                Email = configuration["Data:AdminUser:Email"],
                FirstName = configuration["Data:AdminUser:FirstName"],
                LastName = configuration["Data:AdminUser:LastName"],
                UserName = configuration["Data:AdminUser:UserName"],
                EmailConfirmed = true
            };
            var password = configuration["Data:AdminUser:Password"];
            var role = configuration["Data:AdminUser:Role"];

            if (await userManager.FindByEmailAsync(user.UserName) == null)
            {
                if (await roleManager.FindByNameAsync(role) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }

                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }
        
        public static void SeedData(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<AppDbContext>();
            //якщо немає продуктів - додаємо дефолтні
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product
                    {
                        Id = "1",
                        Name = "БРЯЗКАЛЬЦЕ МЯКА В РУЧКУ FISHER PRICE GH73133 (48ШТ) ЖИРАФ, НА ЗРУЧНОМУ КІЛЬЦІ З КУЛЬКАМИ ВНУТРИ9*13",
                        Description = "Вона виконана з мякого, міцного матеріалу в яскравих райдужних кольорах. Крім мякої частини у брязкальця є невелика прозора ручка з пластмаси, всередині якої знаходяться маленькі кульки. Якщо потрясти іграшку, то вона буде гриміти. Такі звуки привертають увагу малюка і заспокоюють його.",
                        Price = 183.47M,
                        Count = 24
                    },
                    new Product
                    {
                        Id = "2",
                        Name = "БРЯЗКАЛЬЦЕ МЯКА В РУЧКУ FISHER PRICE GH73100(48ШТ) ЖИРАФ, ВСЕРЕДИНІ ПИЩАЛКА 9*28 СМ, НА ПЛАНШЕТКЕ19",
                        Description = "Це нова іграшка Fisher Price, розроблена спеціально для малюків. Вона зроблена з мякого матеріалу в яскравих кольорах, в забарвлює склад додані харчові барвники. Завдяки цьому іграшка безпечна для дитини в самому ранньому віці. Всередині знаходиться елемент, який пищить при натисканні. Знизу доповнена елементом-прорезивателей, за допомогою якого можна чесати ясна, коли зявляються зубки. Розвиває слух, уважність, дрібну моторику і зір. Рекомендується дітям від 0 місяців.",
                        Price = 165.33M,
                        Count = 9
                    });

                context.SaveChanges();
            }
        }

    }
}