using System.ComponentModel.DataAnnotations;

namespace DiplomToyStore.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
