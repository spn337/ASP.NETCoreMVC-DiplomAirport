using DiplomToyStore.Domain.AbstractRepo;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DiplomToyStore.ViewComponents
{
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly ICategoryRepository _repository;
        public CategoriesViewComponent(ICategoryRepository repository)
        {
            _repository = repository;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(_repository.Categories
                .Select(x => x.Name)
                .OrderBy(x => x));
        }
    }
}
