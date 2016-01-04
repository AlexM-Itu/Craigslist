using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Craigslist.Business;
using Craigslist.Domain.Entities;
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
			    Categories = categories,
				Listing = new Listing
				{
					Id = -1
				}
		    });
	    }

	    private List<SelectListItem> RetrieveAllCategories()
	    {
		    var allCategories = lookupManager.GetAllCategories().ToList();
		    return categoriesHelper.GetCategoriesListItems(allCategories);
	    }

	    [HttpPost]
		public ActionResult Publish(ListingPublishingViewModel model, HttpPostedFileBase image = null)
	    {
		    if (ModelState.IsValid)
		    {
			    var removalGuid = Guid.NewGuid();
			    if (image != null)
			    {
				    model.Listing.FeaturedImageMimeType = image.ContentType;
					model.Listing.FeaturedImageData = new byte[image.ContentLength];
				    image.InputStream.Read(model.Listing.FeaturedImageData, 0, image.ContentLength);
			    }

				if (model.Listing.Id == -1)
				{
					model.Listing.Id = 0;
					model.Listing.RemovalGuid = removalGuid.ToString();
					listingsManager.PublishListing(model.Listing);
					emailManager.SendEmail(model.Listing.Contact.Email, ConfigurationManager.AppSettings["Email"], "Control Your Listing", string.Format(@"Hi {0}, 
Thank you for posting your listing {1} on our website. 
If your item is sold out or you just want to delete it for whatever reason, just click the list below: 
http://{2}/Listing/Delete?removalId={3}
If you want to amend your posting, click the link below:
http://{2}/Listing/Edit?removalId={3}", model.Listing.Contact.FirstName, model.Listing.Header, HttpContext.Request.Url.Host, removalGuid));
				}
				else
				{
					listingsManager.UpdateListing(model.Listing);
				}
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

		public ActionResult Delete(string removalId)
        {
			var listing = listingsManager.GetListingsByRemovalGuid(removalId);
            if (listing != null)
                return View(listing);

            return RedirectToAction("List");
        }

        [HttpPost]
        [ActionName("Delete")]
		public ActionResult PerformDeletion(string removalGuid)
        {
            listingsManager.DeactivateListingByRemovalId(removalGuid);

            return View("DeletionConfirmation");
        }

	    public FileContentResult GetFeaturedImage(long listingId)
	    {
			var listing = listingsManager.GetListingsById(listingId);
		    if (listing != null && listing.FeaturedImageData != null)
			    return new FileContentResult(listing.FeaturedImageData, listing.FeaturedImageMimeType);

		    return null;
	    }

	    public ActionResult Edit(string removalId)
	    {
			var listing = listingsManager.GetListingsByRemovalGuid(removalId);
		    if (listing != null)
		    {
				var categories = RetrieveAllCategories();

			    return View("Publish", new ListingPublishingViewModel
			    {
				    Categories = categories,
					Listing = listing
			    });
		    }

		    return RedirectToAction("List");
	    }
    }
}