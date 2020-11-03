using DiplomToyStore.Data.AbstractRepo;
using DiplomToyStore.Models;
using System.Collections.Generic;
using System.Linq;

namespace DiplomToyStore.Data.ConcreteRepo
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Product> GetProducts() 
            => _context.Products;


        public Product GetProductById(int id)
            => _context.Products.FirstOrDefault(p => p.Id == id);
        

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
