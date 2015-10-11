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

		public string ComposeCategoryName(Category catetory)
		{
			string name = string.Empty;
			Category itr = catetory;
		    
			while (itr.ParentId !=null)
			{
				name += " ";
				itr = itr.ParentCategory;
			}

			return string.Format("{0} {1}", name, catetory.Name);
		}
	}
}
