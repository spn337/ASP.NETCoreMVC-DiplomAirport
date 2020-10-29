using DiplomAirport.Models;
using System.Collections.Generic;

namespace DiplomAirport.Data.AbstractRepo
{
    public interface IProductRepository
    {
        bool SaveChanges();
        IEnumerable<Product> GetProducts();
        Product GetProductById(string id);
    }
}
