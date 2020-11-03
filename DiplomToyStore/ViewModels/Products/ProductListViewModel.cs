using DiplomToyStore.Models;
using System.Collections.Generic;

namespace DiplomToyStore.ViewModels.Products
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
    }
}
