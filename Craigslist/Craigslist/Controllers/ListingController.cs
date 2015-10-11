using System.Web.Mvc;
using System.Web.Routing;
using Craigslist.Business;
using Craigslist.Domain.Entities;

namespace Craigslist.Controllers
{
    public class ListingController : Controller
    {
		private readonly ListingsManager manager = new ListingsManager();

	    public ViewResult List()
	    {
		    return View();
	    }

	    public ViewResult Publish()
	    {
		    return View();
	    }

		[HttpPost]
	    public ActionResult Publish(Listing listing)
	    {
		    if (ModelState.IsValid)
		    {
			    manager.PublishListing(listing);
				return new RedirectToRouteResult(new RouteValueDictionary());
		    }

			return View();
	    }
    }
}