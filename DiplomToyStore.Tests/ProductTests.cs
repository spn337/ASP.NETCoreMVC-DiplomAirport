using DiplomToyStore.Controllers;
using DiplomToyStore.Data.AbstractRepo;
using DiplomToyStore.Models;
using DiplomToyStore.ViewModels;
using DiplomToyStore.ViewModels.Products;
using Moq;
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
    }
}
