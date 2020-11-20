using DiplomToyStore.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace DiplomToyStore.ViewModels.Products
{
    public class CreateProductViewModel
    {
        [Required(ErrorMessage = "Please enter a product name")]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter a positive count")]

        public int Count { get; set; } = 1;

        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public IFormFile Photo { get; set; }
    }
}
