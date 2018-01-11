using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.Educacion
{
	public partial class Sector
	{
		public static List<Sector> GetAll()
		{
			return
				(
				from query in Query.GetSectores()
				select query
				).ToList<Sector>();
		}
	}
}