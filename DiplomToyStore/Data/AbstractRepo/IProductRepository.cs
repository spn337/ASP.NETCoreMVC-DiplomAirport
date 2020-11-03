using DiplomToyStore.Models;
using System.Linq;

namespace DiplomToyStore.Data.AbstractRepo
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
    }
}
