using System.Collections.Generic;
using System.Web.Mvc;

namespace Craigslist.Models.Navigation
{
	public class NavigationViewModel
	{
		public List<SelectListItem> Categories { get; set; }

		public long? SelectedCategoty { get; set; }

		public string SearchQuery { get; set; }
	}
}