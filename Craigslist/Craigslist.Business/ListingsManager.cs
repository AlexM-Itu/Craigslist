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

		public List<Listing> GetListingsByCategoryId(long? categoryId)
		{
			using (var domain = new CraigslistDomain())
			{
				return domain
					.Listings
					.Include(l => l.Category)
					.Include("Category.ParentCategory")
					.Include(l => l.Contact)
					.Where(listing => categoryId == null || listing.CategoryId == categoryId)
					.ToList();
			}
		}

		public Listing GetListingsById(long listingId)
		{
			using (var domain = new CraigslistDomain())
			{
				return domain
					.Listings
					.Include(listing => listing.Contact)
					.Include(listing => listing.Category)
					.FirstOrDefault(listing => listing.Id == listingId);
			}
		}
	}
}
