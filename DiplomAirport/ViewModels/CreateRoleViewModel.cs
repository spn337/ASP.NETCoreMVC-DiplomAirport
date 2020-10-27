using System.ComponentModel.DataAnnotations;

namespace DiplomAirport.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
