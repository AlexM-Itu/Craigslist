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
	}
}
