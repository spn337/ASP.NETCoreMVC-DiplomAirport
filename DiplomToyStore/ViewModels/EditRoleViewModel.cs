using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiplomToyStore.ViewModels
{
    public class EditRoleViewModel
    {
        public string Id { get; set; }

        [Required]
        public string RoleName { get; set; }

        public List<string> Users { get; set; }

        public EditRoleViewModel()
        {
            Users = new List<string>();
        }
    }
}
