using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Craigslist.Domain.Entities;

namespace Craigslist.Helpers
{
	public class CategoriesHelper
	{
		private void AddCategoriesToList(List<Category> categories, List<SelectListItem> items, int level)
		{
			foreach (var category in categories.OrderBy(cat => cat.Name))
			{
				string prefix = string.Empty;
				for (int i = 0; i < level; i++)
					prefix += "-";

				items.Add(new SelectListItem
				{
					Value = category.Id.ToString(),
					Disabled = category.SubCategories.Any(),
					Text = prefix + category.Name
				});

				AddCategoriesToList(category.SubCategories, items, level + 1);
			}
		}

		public List<SelectListItem> GetCategoriesListItems(List<Category> categories)
		{
			var items = new List<SelectListItem>();
			AddCategoriesToList(categories.Where(cat => cat.ParentId == null).ToList(), items, 0);
			items.Insert(0, new SelectListItem
			{
				Text = "Select a category", 
				Value = null,
				Disabled = true,
				Selected = true
			});

			return items;
		}
	}
}