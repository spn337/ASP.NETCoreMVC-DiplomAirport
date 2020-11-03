using System.ComponentModel.DataAnnotations;

namespace DiplomToyStore.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
