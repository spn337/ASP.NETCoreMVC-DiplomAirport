using System.ComponentModel.DataAnnotations;

namespace DiplomToyStore.ViewModels
{
    public class SendEmailViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
