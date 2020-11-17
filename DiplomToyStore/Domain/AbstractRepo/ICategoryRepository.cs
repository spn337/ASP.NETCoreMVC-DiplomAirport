using DiplomToyStore.Models;
using System.Collections.Generic;

namespace DiplomToyStore.Domain.AbstractRepo
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
    }
}
