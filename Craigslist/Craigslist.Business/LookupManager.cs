using System.Collections.Generic;
using System.Linq;
using Craigslist.Domain;
using Craigslist.Domain.Entities;

namespace Craigslist.Business
{
	public class LookupManager
	{
		public List<Category> GetAllCategories()
		{
			using (var domain = new CraigslistDomain())
			{
				var categories = domain.Categories.ToList();

				foreach (var category in categories)
				{
					category.SubCategories = categories.Where(cat => cat.ParentId == category.Id).ToList();
				}

				return categories;
			}
		}
	}
}
