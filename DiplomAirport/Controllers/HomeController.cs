using DiplomAirport.Data.AbstractRepo;
using DiplomAirport.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;

namespace DiplomAirport.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _repository;
        public HomeController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Index() 
            => View(new ProductListViewModel
            {
                Products = _repository.GetProducts()
            });
    }
}
