using DiplomToyStore.Domain.AbstractRepo;
using DiplomToyStore.Models;
using System.Collections.Generic;

namespace DiplomToyStore.Domain.ConcreteRepo
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Category> Categories => _context.Categories;

        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }
    }
}
