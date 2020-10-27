using DiplomAirport.Data.AbstractRepo;
using DiplomAirport.Models;
using System.Collections.Generic;

namespace DiplomAirport.Data.ConcreteRepo
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Product> Products
        {
            get
            {
                return _context.Products;
            }
        }
    }
}
