using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Craigslist.Domain;
using Craigslist.Domain.Entities;

namespace Craigslist.Business
{
	public class ListingsManager
	{
		public void PublishListing(Listing listing)
		{
			using (var domain = new CraigslistDomain())
			{
				domain.Listings.Add(listing);
				domain.SaveChanges();
			}
		}

		public List<Listing> GetListingsByCategory(string category)
		{
			using (var domain = new CraigslistDomain())
			{
				return domain
					.Listings
					.Where(listing => category == null || listing.Category.Name == category)
					.Include(l => l.Category)
					.Include(l => l.Contact)
					.ToList();
			}
		}
	}
}
