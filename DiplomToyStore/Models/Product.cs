using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiplomToyStore.Models
{
    [Table("tblProducts")]
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Please enter a product name")]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter a positive count")]

        public int Count { get; set; } = 1;

        public virtual ICollection<ProductPhoto> Children { get; set; }


        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
