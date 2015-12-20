using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Craigslist.Domain.Entities
{
	public class Contact : EntityBase
    {
        public long Id { get; set; }

        [Required (ErrorMessage = "First Name is required!")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required (ErrorMessage = "Last Name is required!")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter a vaild Phone number!")]
        [StringLength(10),MinLength(10)]
        public string Phone { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

		public DateTime? Updated { get; set; }

		public HashSet<Listing> Listings { get; set; }

		public Contact()
		{
			Listings = new HashSet<Listing>();
		}
    }
}
