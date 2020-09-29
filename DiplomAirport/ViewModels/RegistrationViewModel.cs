using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DiplomAirport.ViewModels
{
    public class RegistrationViewModel
    {
        [Required]
        [Display(Name = "First name")]
        [RegularExpression(@"[a-zA-z]{2,20}",
            ErrorMessage = "Only letters from 2 to 20 symbols")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        [RegularExpression(@"[a-zA-z]{2,20}",
            ErrorMessage = "Only letters from 2 to 20 symbols")]
        public string LastName { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(50, 
            ErrorMessage = "Max length is 50")]
        [Remote(action: "IsEmailInUse", controller: "Account")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(50, 
            ErrorMessage = "Max length is 50")]

        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password",
            ErrorMessage = "Password and confirmation password do not match.")]
        [StringLength(50, 
            ErrorMessage = "Max length is 50")]

        public string ConfirmPassword { get; set; }

        [Required]
        [RegularExpression(@"[a-zA-z]{2,20}",
            ErrorMessage = "Only letters from 2 to 20 symbols")]
        public string Country { get; set; }
    }

}
