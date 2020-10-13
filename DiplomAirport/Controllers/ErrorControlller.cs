using Microsoft.AspNetCore.Mvc;

namespace DiplomAirport.Controllers
{
    public class ErrorControlller : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeBody(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the resource you requested could not be found";
                    break;
            }
            return View("NotFound");
        }
    }
}
