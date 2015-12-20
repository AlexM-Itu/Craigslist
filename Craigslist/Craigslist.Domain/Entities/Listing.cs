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

        [Required (ErrorMessage = "Title field is required!")]
        [StringLength(200)]
        public string Header { get; set; }

        [Required (ErrorMessage = "Description of the listing is required!")]
        public string Body { get; set; }

        [Required(ErrorMessage = "Please enter a valid price!")]
        public decimal Price { get; set; }

		public string RemovalGuid { get; set; }

	    public bool IsActive { get; set; }

		public DateTime? Updated { get; set; }

        public virtual Category Category { get; set; }

        public virtual Contact Contact { get; set; }
    }
}
