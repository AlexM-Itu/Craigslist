using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Craigslist.Domain.Entities
{
	public class Listing : EntityBase
    {
        public long Id { get; set; }

		[ForeignKey("Category")]
        public int CategoryId { get; set; }

		[ForeignKey("Contact")]
        public long ContactId { get; set; }

        [Required]
        [StringLength(200)]
        public string Header { get; set; }

        [Required]
        public string Body { get; set; }

		public decimal Price { get; set; }

		public DateTime? Updated { get; set; }

        public virtual Category Category { get; set; }

        public virtual Contact Contact { get; set; }
    }
}
