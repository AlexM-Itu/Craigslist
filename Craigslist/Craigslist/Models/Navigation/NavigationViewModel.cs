using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Craigslist.Models.Navigation
{
	public class NavigationViewModel
	{
		public List<SelectListItem> Categories { get; set; }

		[Required]
		public long? SelectedCategoty { get; set; }
	}
}