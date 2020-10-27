using System.ComponentModel.DataAnnotations;

namespace DiplomAirport.Models
{
    public class Product
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Name can't exceed 30 characters")]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public ushort Price { get; set; }
    }
}
