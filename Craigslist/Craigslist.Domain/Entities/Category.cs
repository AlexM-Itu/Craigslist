using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Craigslist.Domain.Entities
{
	public class Category : EntityBase
    {
        public int Id { get; set; }

		[ForeignKey("ParentCategory")]
        public int ParentId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

		public HashSet<Listing> Listings { get; set; }

		public Category ParentCategory { get; set; }

		public Category()
		{
			Listings = new HashSet<Listing>();
		}
    }
}
