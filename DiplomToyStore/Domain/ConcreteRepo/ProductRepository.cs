using DiplomToyStore.Domain.AbstractRepo;
using DiplomToyStore.Models;
using System.Linq;

namespace DiplomToyStore.Domain.ConcreteRepo
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public IQueryable<Product> Products => _context.Products;

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }
    }
}
