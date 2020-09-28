using Microsoft.AspNetCore.Identity;

namespace DiplomAirport.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
    }
}
