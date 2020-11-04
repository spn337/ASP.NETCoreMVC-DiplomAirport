using DiplomToyStore.Data.AbstractRepo;
using DiplomToyStore.ViewModels;
using DiplomToyStore.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DiplomToyStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _repository;
        public int PageSize = 1;
        public HomeController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ViewResult Index(int productPage = 1)
            => View(new ProductListViewModel
            {
                Products = _repository.Products
                    .OrderBy(p => p.Id)
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize),
                PaggingInfo = new PageViewModel
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = _repository.Products.Count()
                }
            });
    }
}
