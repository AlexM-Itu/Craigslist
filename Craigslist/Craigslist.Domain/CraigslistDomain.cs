using System.Data.Entity;
using Craigslist.Domain.Entities;

namespace Craigslist.Domain
{
	public partial class CraigslistDomain : DbContext
	{
		public virtual DbSet<Category> Categories { get; set; }

		public virtual DbSet<Contact> Contacts { get; set; }
		
		public virtual DbSet<Listing> Listings { get; set; }
	}
}
