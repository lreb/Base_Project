using BaseProject.API.Domain;
using System.Collections.Generic;

namespace BaseProject.API.Persistence.Seeds.SeedEntity
{
	public static class DepartmentSeed
	{
		public static List<Department> ItemList()
		{
			return new List<Department>()
			{

				new Department { Id = 1, Name= "A", Description="" },
				new Department { Id = 2, Name = "B", Description="" },
				new Department { Id = 3, Name = "C", Description="" }
			};
		}
	}
}
