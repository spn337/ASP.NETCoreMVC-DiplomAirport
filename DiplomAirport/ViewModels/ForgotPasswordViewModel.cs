using System.ComponentModel.DataAnnotations;

namespace DiplomAirport.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
