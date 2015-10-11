using System.Linq;
using System.Web.Mvc;
using Craigslist.Business;

namespace Craigslist.Controllers
{
    public class NavigationController : Controller
    {
	    private readonly LookupManager lookupManager = new LookupManager();

		public ActionResult Index()
        {
			var allCategories = lookupManager.GetAllCategories().ToList();
			var categories = allCategories.Select(catetory => new SelectListItem
			{
				Value = catetory.Id.ToString(),
				Text = lookupManager.ComposeCategoryName(catetory)
			}).ToList();

            return View(categories);
        }
    }
}