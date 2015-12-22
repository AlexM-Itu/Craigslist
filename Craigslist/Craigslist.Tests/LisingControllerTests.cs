using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Craigslist.Controllers;
using Craigslist.Business;
using Craigslist.Domain.Entities;

namespace Craigslist.Tests
{
    [TestClass]
    public class LisingControllerTests
    {
        // private readonly ListingController listingController = new ListingController();
        private readonly ListingsManager listingManager = new ListingsManager();
        private readonly Craigslist.Domain.CraigslistDomain domain = new Domain.CraigslistDomain();

        [TestMethod]
        public void PublishTest()
        {
	        Listing listing = new Listing
	        {
		        Body = "Publist test body",
		        Header = "PublishTest",
		        Price = 300,
		        CategoryId = 3,
		        Category = null,
		        RemovalGuid = Guid.NewGuid().ToString(),
		        Contact = new Contact
		        {
			        FirstName = "Test_FName",
			        LastName = "Test_LName",
			        Phone = "9999999999",
			        Email = "test@gmail.com",
			        Updated = null
		        },
		        IsActive = false,
		        Id = 0,
		        Updated = null
	        };

            listingManager.PublishListing(listing);

            var added_listing = domain.Listings
                .Where(e => e.Body == listing.Body &&
                            e.Header == listing.Header &&
                            e.Price == listing.Price &&
                            e.CategoryId == listing.CategoryId &&
                            e.ContactId == listing.ContactId).Single();

            Assert.IsNotNull(added_listing);

            domain.Listings.Remove(added_listing);
        }

        [ClassCleanup]
        public static void cleanUp()
        {
        }
    }
}
