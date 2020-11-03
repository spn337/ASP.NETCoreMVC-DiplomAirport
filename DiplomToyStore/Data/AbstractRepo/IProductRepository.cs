using DiplomToyStore.Models;
using System.Collections.Generic;

namespace DiplomToyStore.Data.AbstractRepo
{
    public interface IProductRepository
    {
        bool SaveChanges();
        IEnumerable<Product> GetProducts();
        Product GetProductById(int id);
    }
}
