using DiplomToyStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomToyStore.Domain
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
            context.Database.Migrate();


            var categories = new Dictionary<string, string>()
            {
                ["Dolls"] = "Ляльки",
                ["BabyDolls"] = "Пупси",
                ["Rattles"] = "Брязкальця",
                ["WheelChair"] = "Каталочки",
                ["Constructors"] = "Конструктори",
                ["Rollers"] = "Ролики",
                ["Scooters"] = "Самокати"
            };

            #region SeedCategory

            if (!context.Categories.Any())
            {
                foreach (var category in categories)
                {
                    context.Categories.Add(new Category { Name = category.Value });
                }
                context.SaveChanges();
            }

            #endregion

            #region SeedProducts

            if (!context.Products.Any())
            {
                for (int i = 0; i < 10; i++)
                {
                    context.Products.AddRange(
                        new Product
                        {
                            Name = "ІГРОВИЙ НАБІР ЛЯЛЬКА ТИПУ БАРБІ GINA GN5012 (72ШТ|2) ЧАРІВНА ПРИНЦЕСА, 3 ВИДИ МІКС, СУКНЯ В ПАЄТКАХ, В КОР.14*5*32СМ",
                            Description = "У цієї лялечки є одна особливість! У неї волосся пофарбоване під колір сукні! Такий незвичайний і ефектний образ виділяє її серед інших. Її плаття мерехтить, завдяки яскравим паєткам. Можна вибрати лялечку в блакитній, золотистій або фіолетовій сукні. Ігри в ляльки сприяють розвитку дрібної моторики, уяви та комунікативних навичок дитини. Іграшка зроблена з безпечних матеріалів.",
                            Price = 136.42M,
                            Count = 100,
                            CategoryId = context.Categories
                                .FirstOrDefault(x => x.Name == categories["Dolls"])?.Id
                        },
                        new Product
                        {
                            Name = "ІГРАШКА ЛЯЛЬКА АРТ. 802005 BANIEL, ГОЙДАЄТЬСЯ. У БЛІСТЕРІ 19*8,5*26 СМ",
                            Description = "",
                            Price = 203.40M,
                            Count = 21,
                            CategoryId = context.Categories
                                .FirstOrDefault(x => x.Name == categories["BabyDolls"])?.Id
                        },
                        new Product
                        {
                            Name = "БРЯЗКАЛЬЦЕ МЯКА В РУЧКУ FISHER PRICE GH73133 (48ШТ) ЖИРАФ, НА ЗРУЧНОМУ КІЛЬЦІ З КУЛЬКАМИ ВНУТРИ9*13",
                            Description = "Вона виконана з мякого, міцного матеріалу в яскравих райдужних кольорах. Крім мякої частини у брязкальця є невелика прозора ручка з пластмаси, всередині якої знаходяться маленькі кульки. Якщо потрясти іграшку, то вона буде гриміти. Такі звуки привертають увагу малюка і заспокоюють його.",
                            Price = 183.47M,
                            Count = 24,
                            CategoryId = context.Categories
                                .FirstOrDefault(x => x.Name == categories["Rattles"])?.Id
                        },
                        new Product
                        {
                            Name = "ЗАВОДНИЙ ВЕРТОЛІТ 880B (480ШТ) 3 ВИДИ, НА КОЛІЩАТКАХ, В ПАКЕТІ",
                            Description = "Вона виконана з мякого, міцного матеріалу в яскЗаводний літачок - прекрасний подарунок для дітей, які обожнюють гратися різними видами транспорту. Літачок у яскравому кольорі обовязково сподобається кожному невгамовному малюку. Виготовлений з полімерних матеріалів та є безпечним для здоровя дитини. Призначена іграшка для дітей віком від 3-х років. В наявності 3 види на колесиках.",
                            Price = 18.90M,
                            Count = 100,
                            CategoryId = context.Categories
                                .FirstOrDefault(x => x.Name == categories["WheelChair"])?.Id
                        },
                        new Product
                        {
                            Name = "КОНСТРУКТОР LEPIN CITY 02019 (26ШТ|2) ПОГРАБУВАННЯ НА БУЛЬДОЗЕРІ, 606ДЕТ., В СОБР.КОРОБЦІ 57*32*7СМ",
                            Description = "",
                            Price = 758.18M,
                            Count = 100,
                            CategoryId = context.Categories
                                .FirstOrDefault(x => x.Name == categories["Constructors"])?.Id
                        },
                        new Product
                        {
                            Name = "РОЛИКИ SCALE SPORTS LF 905 S, ФИОЛЕТОВЫЙ",
                            Description = "Опис і переваги роликів Scale Sport: виріб торгової марки Scale Sport; устілка 18 - 20,5 см можливість тривалої експлуатації -кнопкова система регулювання на 4 розміру; поліпропіленові оболонки і манжети;",
                            Price = 758.18M,
                            Count = 5,
                            CategoryId = context.Categories
                                .FirstOrDefault(x => x.Name == categories["Rollers"])?.Id
                        },
                        new Product
                        {
                            Name = "САМОКАТ ДИТЯЧИЙ 4-Х КОЛІС. SK20160 (6ШТ) MIX КВІТІВ, КОЛЕСА PU 135MM*40 ММ ЗІ СВІТЛОМ",
                            Description = "Максимальне навантаження, кг 50 кг, Вік: 3, Колір: MIX кольорів",
                            Price = 1267.92M,
                            Count = 5,
                            CategoryId = context.Categories
                                .FirstOrDefault(x => x.Name == categories["Scooters"])?.Id
                        });
                    context.SaveChanges();
                }
            }

            #endregion
        }

    }
}
