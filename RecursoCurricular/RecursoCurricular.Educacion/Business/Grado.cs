using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.Educacion
{
	public partial class Grado
	{
		public static List<Grado> GetAll()
		{
			return
				(
				from query in Query.GetGrados()
				select query
				).ToList<Grado>();
		}
	}
}