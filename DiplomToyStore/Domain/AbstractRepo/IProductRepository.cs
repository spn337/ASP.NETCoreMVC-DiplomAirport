using DiplomToyStore.Models;
using System.Linq;

namespace DiplomToyStore.Domain.AbstractRepo
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
        void AddProduct(Product product);
    }
}
