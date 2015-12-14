using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Craigslist.Business;
using Craigslist.Helpers;
using Craigslist.Models.Listings;

namespace Craigslist.Controllers
{
    public class ListingController : Controller
    {
		private readonly ListingsManager listingsManager = new ListingsManager();
		private readonly LookupManager lookupManager = new LookupManager();
		private readonly CategoriesHelper categoriesHelper = new CategoriesHelper();
		private readonly EmailManager emailManager = new EmailManager();

        public ViewResult List(int? page, long? categoryId = null , string q = null)
	    {
		    return View(listingsManager.GetListingsByCategoryIdAndSearchQuery(page, categoryId, q));
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
			    var listingId = listingsManager.PublishListing(model.Listing);
				emailManager.SendEmail(model.Listing.Contact.Email, ConfigurationManager.AppSettings["Email"], "Control Your Listing", string.Format(@"Hi {0}, 
Thank you for posting your listing {1} on our website. 
If your item is sold out or you just want to delete it for whatever reason, just click the list below: 
http://{2}/Delete/{3}", model.Listing.Contact.FirstName, model.Listing.Header, HttpContext.Request.Url.Host, listingId));
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

        public ActionResult Delete(long listingId)
        {
            var listing = listingsManager.GetListingsById(listingId);
            if (listing != null)
                return View(listing);

            return RedirectToAction("List");
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult PerformDeletion(long id)
        {
            listingsManager.DeactivateListingById(id);

            return View("DeletionConfirmation");
        }
    }
}