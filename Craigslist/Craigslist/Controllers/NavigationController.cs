using System.Linq;
using System.Web.Mvc;
using Craigslist.Business;
using Craigslist.Helpers;

namespace Craigslist.Controllers
{
    public class NavigationController : Controller
    {
	    private readonly LookupManager lookupManager = new LookupManager();
		private readonly CategoriesHelper categoriesHelper = new CategoriesHelper();

		public ActionResult Index()
        {
			var allCategories = lookupManager.GetAllCategories().ToList();
			return View(categoriesHelper.GetCategoriesListItems(allCategories));
        }
    }
}