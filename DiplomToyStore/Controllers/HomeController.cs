using DiplomToyStore.Data.AbstractRepo;
using Microsoft.AspNetCore.Mvc;

namespace DiplomToyStore.Controllers
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
            => View(_repository.Products);
    }
}
