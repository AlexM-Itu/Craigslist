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
				return domain.Categories.ToList();
			}
		} 
	}
}
