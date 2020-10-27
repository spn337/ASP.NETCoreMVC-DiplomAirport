﻿using DiplomAirport.Data.AbstractRepo;
using DiplomAirport.Models;
using System;
using System.Collections.Generic;

namespace DiplomAirport.Data.ConcreteRepo
{
    public class MockRepository : IProductRepository
    {
        public IEnumerable<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Id="1",
                    Name = "БРЯЗКАЛЬЦЕ МЯКА В РУЧКУ FISHER PRICE GH73133 (48ШТ) ЖИРАФ, НА ЗРУЧНОМУ КІЛЬЦІ З КУЛЬКАМИ ВНУТРИ9*13",
                    Description = "Вона виконана з мякого, міцного матеріалу в яскравих райдужних кольорах. Крім мякої частини у брязкальця є невелика прозора ручка з пластмаси, всередині якої знаходяться маленькі кульки. Якщо потрясти іграшку, то вона буде гриміти. Такі звуки привертають увагу малюка і заспокоюють його.",
                    Price = 183.47M,
                    Count = 24
                },
                 new Product
                {
                    Id="2",
                    Name = "БРЯЗКАЛЬЦЕ МЯКА В РУЧКУ FISHER PRICE GH73100(48ШТ) ЖИРАФ, ВСЕРЕДИНІ ПИЩАЛКА 9*28 СМ, НА ПЛАНШЕТКЕ19",
                    Description = "Це нова іграшка Fisher Price, розроблена спеціально для малюків. Вона зроблена з мякого матеріалу в яскравих кольорах, в забарвлює склад додані харчові барвники. Завдяки цьому іграшка безпечна для дитини в самому ранньому віці. Всередині знаходиться елемент, який пищить при натисканні. Знизу доповнена елементом-прорезивателей, за допомогою якого можна чесати ясна, коли зявляються зубки. Розвиває слух, уважність, дрібну моторику і зір. Рекомендується дітям від 0 місяців.",
                    Price = 165.33M,
                    Count = 9
                },
            };
        }
        public Product GetProductById(string id)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
