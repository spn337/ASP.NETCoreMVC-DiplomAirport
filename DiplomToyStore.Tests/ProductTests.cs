using DiplomToyStore.Controllers;
using DiplomToyStore.Domain.AbstractRepo;
using DiplomToyStore.Models;
using DiplomToyStore.ViewComponents;
using DiplomToyStore.ViewModels;
using DiplomToyStore.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using NLog.Web.LayoutRenderers;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DiplomToyStore.Tests
{
    public class ProductTests
    {
        [Fact]
        public void Can_Paginate()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product{ Id = 1, Name = "Toy1" },
                new Product{ Id = 2, Name = "Toy2" },
                new Product{ Id = 3, Name = "Toy3" },
                new Product{ Id = 4, Name = "Toy4" },
                new Product{ Id = 5, Name = "Toy5" },
            }.AsQueryable());
            HomeController controller = new HomeController(mock.Object)
            {
                PageSize = 3
            };

            ProductListViewModel result = controller.Index(null, 2).ViewData.Model
                as ProductListViewModel;

            PageViewModel paggingInfo = result.PaggingInfo;
            Assert.Equal(2, paggingInfo.CurrentPage);
            Assert.Equal(3, paggingInfo.ItemsPerPage);
            Assert.Equal(5, paggingInfo.TotalItems);
            Assert.Equal(2, paggingInfo.TotalPages);
        }

        [Fact]
        public void Can_Filter_By_Category()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product{ Id = 1, Name = "Toy1", Category = new Category{Name = "Cat1" }},
                new Product{ Id = 2, Name = "Toy2", Category = new Category{Name = "Cat2" }},
                new Product{ Id = 3, Name = "Toy3", Category = new Category{Name = "Cat1" } },
                new Product{ Id = 4, Name = "Toy4", Category = new Category{Name = "Cat2" } },
                new Product{ Id = 5, Name = "Toy5", Category = new Category{Name = "Cat3" } },
            }.AsQueryable());

            HomeController controller = new HomeController(mock.Object)
            {
                PageSize = 3
            };

            ProductListViewModel result = controller.Index("Cat2", 1).ViewData.Model
                as ProductListViewModel;

            Product[] products = result.Products.ToArray();
            Assert.Equal(2, products.Length);
            Assert.True(products[0].Name == "Toy2" && products[0].Category.Name == "Cat2");
            Assert.True(products[1].Name == "Toy4" && products[0].Category.Name == "Cat2");
        }

        [Fact]
        public void Can_Select_Categories()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product{ Id = 1, Name = "Toy1", Category = new Category{Name = "Cat1" }},
                new Product{ Id = 2, Name = "Toy2", Category = new Category{Name = "Cat2" }},
                new Product{ Id = 3, Name = "Toy3", Category = new Category{Name = "Cat1" } },
                new Product{ Id = 4, Name = "Toy4", Category = new Category{Name = "Cat2" } },
                new Product{ Id = 5, Name = "Toy5", Category = new Category{Name = "Cat3" } },
            }.AsQueryable());

            CategoriesViewComponent categories = new CategoriesViewComponent(mock.Object);

            string[] result = ((IEnumerable<string>)(categories.Invoke() as ViewViewComponentResult)
                .ViewData.Model).ToArray();

            Assert.True(Enumerable.SequenceEqual(new string[] { "Cat1", "Cat2", "Cat3" }, result));
        }

        [Fact]
        public void Generate_Category_Specific_Product_Count()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product{ Id = 1, Name = "Toy1", Category = new Category{Name = "Cat1" }},
                new Product{ Id = 2, Name = "Toy2", Category = new Category{Name = "Cat2" }},
                new Product{ Id = 3, Name = "Toy3", Category = new Category{Name = "Cat1" } },
                new Product{ Id = 4, Name = "Toy4", Category = new Category{Name = "Cat2" } },
                new Product{ Id = 5, Name = "Toy5", Category = new Category{Name = "Cat3" } },
            }.AsQueryable());

            HomeController controller = new HomeController(mock.Object)
            {
                PageSize = 3
            };

            Func<ViewResult, ProductListViewModel> GetModel
                = result => result?.ViewData?.Model as ProductListViewModel;

            int? res1 = GetModel(controller.Index("Cat1"))?.PaggingInfo.TotalItems;
            int? res2 = GetModel(controller.Index("Cat2"))?.PaggingInfo.TotalItems;
            int? res3 = GetModel(controller.Index("Cat3"))?.PaggingInfo.TotalItems;
            int? resAll = GetModel(controller.Index(null))?.PaggingInfo.TotalItems;

            Assert.Equal(2, res1);
            Assert.Equal(2, res2);
            Assert.Equal(1, res3);
            Assert.Equal(5, resAll);
        }
    }
}
