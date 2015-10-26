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

		public ActionResult Index(long? categoryId)
        {
			var allCategories = lookupManager.GetAllCategories().ToList();
			return View(new NavigationViewModel {SelectedCategoty = categoryId, Categories = categoriesHelper.GetCategoriesListItems(allCategories) });
        }

		[HttpPost]
	    public ActionResult AppySelection(NavigationViewModel model)
		{
		    return RedirectToAction("List", "Listing", new {categoryId = model.SelectedCategoty});
	    }

	    public ActionResult ClearSearch()
	    {
			return RedirectToAction("List", "Listing", new { categoryId = (long?) null });
	    }
    }
}