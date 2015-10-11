using System.Linq;
using System.Web.Mvc;
using Craigslist.Business;
using Craigslist.Models.Listings;

namespace Craigslist.Controllers
{
    public class ListingController : Controller
    {
		private readonly ListingsManager listingsManager = new ListingsManager();
		private readonly LookupManager lookupManager = new LookupManager();

	    public ViewResult List(string category = null)
	    {
		    return View(listingsManager.GetListingsByCategory(category));
	    }

	    public ViewResult Publish()
	    {
		    var allCategories = lookupManager.GetAllCategories().ToList();

		    return View(new ListingPublishingViewModel
		    {
			    Categories = allCategories.Select(catetory => new SelectListItem
			    {
				    Value = catetory.Id.ToString(),
				    Disabled = allCategories.Any(c => c.ParentId == catetory.Id),
				    Text = lookupManager.ComposeCategoryName(catetory)
			    }).ToList()
		    });
	    }

	    [HttpPost]
	    public ActionResult Publish(ListingPublishingViewModel model)
	    {
		    if (ModelState.IsValid)
		    {
			    listingsManager.PublishListing(model.Listing);
			    return RedirectToAction("List");
		    }

			return View();
	    }
    }
}