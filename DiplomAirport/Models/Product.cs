using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiplomAirport.Models
{
    [Table("tblProducts")]
    public class Product
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Name can't exceed 30 characters")]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Count { get; set; }
    }
}
