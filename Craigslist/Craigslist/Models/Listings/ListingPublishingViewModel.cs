using System.Collections.Generic;
using System.Web.Mvc;
using Craigslist.Domain.Entities;

namespace Craigslist.Models.Listings
{
	public class ListingPublishingViewModel
	{
		public Listing Listing { get; set; }
		
		public List<SelectListItem> Categories { get; set; }
	}
}