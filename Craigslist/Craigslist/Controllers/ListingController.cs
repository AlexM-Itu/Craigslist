using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Craigslist.Business;
using Craigslist.Helpers;
using Craigslist.Models.Listings;
using PagedList;

namespace Craigslist.Controllers
{
    public class ListingController : Controller
    {
		private readonly ListingsManager listingsManager = new ListingsManager();
		private readonly LookupManager lookupManager = new LookupManager();
		private readonly CategoriesHelper categoriesHelper = new CategoriesHelper();

        public ViewResult List(int? page, long? categoryId = null )
	    {
		    return View(listingsManager.GetListingsByCategoryId(page, categoryId));
	    }

	    public ViewResult Publish()
	    {
		    var categories = RetrieveAllCategories();
		    return View(new ListingPublishingViewModel
		    {
			    Categories = categories
		    });
	    }

	    private List<SelectListItem> RetrieveAllCategories()
	    {
		    var allCategories = lookupManager.GetAllCategories().ToList();
		    return categoriesHelper.GetCategoriesListItems(allCategories);
	    }

	    [HttpPost]
	    public ActionResult Publish(ListingPublishingViewModel model)
	    {
		    if (ModelState.IsValid)
		    {
			    listingsManager.PublishListing(model.Listing);
			    return RedirectToAction("List");
		    }

		    model.Categories = RetrieveAllCategories();
			return View(model);
	    }

	    public ActionResult Detail(long listingId)
	    {
		    var listing = listingsManager.GetListingsById(listingId);
		    if (listing == null)
			    return RedirectToAction("List");

		    return View(listing);
	    }
    }
}