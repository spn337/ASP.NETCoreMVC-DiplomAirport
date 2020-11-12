using DiplomToyStore.Models;
using System.Collections.Generic;

namespace DiplomToyStore.Domain.AbstractRepo
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
        Product GetProductById(int id);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
    }
}
