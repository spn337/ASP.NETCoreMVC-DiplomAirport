using DiplomToyStore.Domain.AbstractRepo;
using DiplomToyStore.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public IEnumerable<Product> Products =>
            _context.Products.Include(c => c.Category);

        public Product GetProductById(int id) => 
            _context.Products.Include(c => c.Category)
            .First(p => p.Id == id);

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            var p = _context.Products.Find(product.Id);
            p.Name = product.Name;
            p.Description = product.Description;
            p.Price = product.Price;
            p.Count = product.Count;
            p.CategoryId = product.CategoryId;
            _context.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }
    }
}
