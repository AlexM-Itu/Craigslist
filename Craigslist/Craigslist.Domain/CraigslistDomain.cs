using Craigslist.Domain.Entities;

namespace Craigslist.Domain
{
	using System;
	using System.Data.Entity;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;

	public partial class CraigslistDomain : DbContext
	{
		public CraigslistDomain()
			: base("name=CraigslistDomain")
		{
		}

		public virtual DbSet<Category> Categories { get; set; }
		public virtual DbSet<Contact> Contacts { get; set; }
		public virtual DbSet<Listing> Listings { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>()
				.Property(e => e.Name)
				.IsUnicode(false);

			modelBuilder.Entity<Category>()
				.HasMany(e => e.Categories1)
				.WithRequired(e => e.Category1)
				.HasForeignKey(e => e.ParentId);

			modelBuilder.Entity<Category>()
				.HasMany(e => e.Listings)
				.WithRequired(e => e.Category)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Contact>()
				.Property(e => e.FirstName)
				.IsUnicode(false);

			modelBuilder.Entity<Contact>()
				.Property(e => e.LastName)
				.IsUnicode(false);

			modelBuilder.Entity<Contact>()
				.Property(e => e.Phone)
				.IsUnicode(false);

			modelBuilder.Entity<Contact>()
				.Property(e => e.Email)
				.IsUnicode(false);

			modelBuilder.Entity<Contact>()
				.HasMany(e => e.Listings)
				.WithRequired(e => e.Contact)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Listing>()
				.Property(e => e.Header)
				.IsUnicode(false);

			modelBuilder.Entity<Listing>()
				.Property(e => e.Body)
				.IsUnicode(false);
		}
	}
}
