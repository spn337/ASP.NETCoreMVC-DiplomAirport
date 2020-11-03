using DiplomToyStore.Data.AbstractRepo;
using DiplomToyStore.Models;
using System.Linq;

namespace DiplomToyStore.Data.ConcreteRepo
{
    public class EFProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public EFProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public IQueryable<Product> Products
            => _context.Products;
        
    }
}
