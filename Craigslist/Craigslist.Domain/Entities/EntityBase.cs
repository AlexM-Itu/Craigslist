using System;

namespace Craigslist.Domain.Entities
{
	public class EntityBase
	{
		public DateTime Created { get; set; }

		public EntityBase()
		{
			Created = DateTime.Now;
		}
	}
}
