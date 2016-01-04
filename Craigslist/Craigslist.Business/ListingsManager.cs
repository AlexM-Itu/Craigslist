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
				listing.IsActive = true;
				domain.Listings.Add(listing);
				domain.SaveChanges();
			}
		}

		public void UpdateListing(Listing listing)
		{
			using (var domain = new CraigslistDomain())
			{
				var currentLising = domain.Listings.FirstOrDefault(l => l.Id == listing.Id);
				currentLising.Header = listing.Header;
				currentLising.Body = listing.Body;
				currentLising.CategoryId = listing.CategoryId;
				currentLising.FeaturedImageData = listing.FeaturedImageData;
				currentLising.FeaturedImageMimeType = listing.FeaturedImageMimeType;
				currentLising.Price = listing.Price;

				currentLising.Contact.FirstName = listing.Contact.FirstName;
				currentLising.Contact.LastName = listing.Contact.LastName;
				currentLising.Contact.Phone = listing.Contact.Phone;
				currentLising.Contact.Email = listing.Contact.Email;
				domain.SaveChanges();
			}
		}

		public IPagedList<Listing> GetListingsByCategoryIdAndSearchQuery(int? page, long? categoryId, string q)
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
                    .Where(listing => listing.IsActive && (categoryId == null || listing.CategoryId == categoryId) && (q == null || listing.Header.Contains(q)))
                    .OrderByDescending(l => l.Created)
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

		public Listing GetListingsByRemovalGuid(string removalId)
		{
			using (var domain = new CraigslistDomain())
			{
				return domain
					.Listings
					.Include(listing => listing.Contact)
					.Include(listing => listing.Category)
					.FirstOrDefault(listing => listing.RemovalGuid == removalId && listing.IsActive);
			}
		}

		public void DeactivateListingByRemovalId(string removalId)
	    {
            using (var domain = new CraigslistDomain())
            {
                var listingToDeactivate = domain
                    .Listings
                    .FirstOrDefault(listing => listing.RemovalGuid == removalId);

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
