using Microsoft.AspNetCore.Mvc;

namespace DiplomAirport.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
