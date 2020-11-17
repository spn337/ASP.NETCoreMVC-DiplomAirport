using DiplomToyStore.Models;
using System.Collections.Generic;

namespace DiplomToyStore.ViewModels.Products
{
    public class HomePageViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PageViewModel PaggingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}
