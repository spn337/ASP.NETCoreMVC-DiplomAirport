using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiplomAirport.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

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

        public string UserName { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(50,
             ErrorMessage = "Max length is 50")]
        public string Email { get; set; }


        [Required]
        [RegularExpression(@"[a-zA-z]{2,20}",
           ErrorMessage = "Only letters from 2 to 20 symbols")]
        public string Country { get; set; }


        public IList<string> Roles { get; set; }
        public IList<string> Claims { get; set; }

        public EditUserViewModel()
        {
            Roles = new List<string>();
            Claims = new List<string>();
        }
    }
}
