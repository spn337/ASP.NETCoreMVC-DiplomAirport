using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace DiplomAirport.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        public IActionResult Login(string username, string password)
        {
            
            return RedirectToAction("Index");
        }

        public IActionResult Register(string username, string password)
        {

            return RedirectToAction("Index");
        }
    }
}
