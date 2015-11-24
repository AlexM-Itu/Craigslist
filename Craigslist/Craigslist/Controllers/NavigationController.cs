using System.Linq;
using System.Web.Mvc;
using Craigslist.Business;
using Craigslist.Helpers;
using Craigslist.Models.Navigation;

namespace Craigslist.Controllers
{
    public class NavigationController : Controller
    {
	    private readonly LookupManager lookupManager = new LookupManager();
		private readonly CategoriesHelper categoriesHelper = new CategoriesHelper();

		public ActionResult Index(long? categoryId, string q)
        {
			var allCategories = lookupManager.GetAllCategories().ToList();
			return View(new NavigationViewModel {SelectedCategoty = categoryId, SearchQuery = q, Categories = categoriesHelper.GetCategoriesListItems(allCategories) });
        }

		[HttpPost]
	    public ActionResult AppySelection(NavigationViewModel model)
		{
			return RedirectToAction("List", "Listing", new {categoryId = model.SelectedCategoty, q = model.SearchQuery});
		}

	    public ActionResult ClearSearch()
	    {
			return RedirectToAction("List", "Listing", new { categoryId = (long?) null });
	    }
    }
}