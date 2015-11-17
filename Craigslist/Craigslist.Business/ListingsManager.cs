using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Craigslist.Domain;
using Craigslist.Domain.Entities;
using PagedList;
using System.Configuration;
using System;


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

		public IPagedList<Listing> GetListingsByCategoryId(int? page, long? categoryId)
		{
			using (var domain = new CraigslistDomain())
			{
                int pageSize = int.Parse(ConfigurationManager.AppSettings["pageSize"]);
                int pageNumber = (page ?? 1);
				return domain
					.Listings
					.Include(l => l.Category)
					.Include("Category.ParentCategory")
					.Include(l => l.Contact)
                    .Where(listing => listing.IsActive && (categoryId == null || listing.CategoryId == categoryId))
                    .OrderBy(l => l.Created)
					.ToPagedList(pageNumber, pageSize);
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
					.FirstOrDefault(listing => listing.Id == listingId && listing.IsActive);
			}
		}

	    public void DeactivateListingById(long listingId)
	    {
            using (var domain = new CraigslistDomain())
            {
                var listingToDeactivate = domain
                    .Listings
                    .FirstOrDefault(listing => listing.Id == listingId);

                if (listingToDeactivate != null)
                {
                    listingToDeactivate.IsActive = false;
                    listingToDeactivate.Updated = DateTime.Now;
                    domain.SaveChanges();
                }
            }
        }
	}
}
