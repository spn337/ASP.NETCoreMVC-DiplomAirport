using DiplomAirport.Data.AbstractRepo;
using DiplomAirport.Models;
using System.Collections.Generic;
using System.Linq;

namespace DiplomAirport.Data.ConcreteRepo
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


        public Product GetProductById(string id)
            => _context.Products.FirstOrDefault(p => p.Id == id);
        

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
