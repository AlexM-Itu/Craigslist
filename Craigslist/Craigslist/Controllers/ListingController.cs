using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Craigslist.Controllers
{
    public class ListingController : Controller
    {
	    public ViewResult List()
	    {
		    return View();
	    }
    }
}