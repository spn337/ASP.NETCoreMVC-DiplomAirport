using DiplomToyStore.Domain.AbstractRepo;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DiplomToyStore.ViewComponents
{
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly IProductRepository _repository;
        public CategoriesViewComponent(IProductRepository repository)
        {
            _repository = repository;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(_repository.Products
                .Select(x => x.Category.Name)
                .Distinct()
                .OrderBy(x => x));
        }
    }
}
