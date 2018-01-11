using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.Educacion
{
	public partial class Ciclo
	{
		public static List<Ciclo> GetAll()
		{
			return
				(
				from query in Query.GetCiclos()
				select query
				).ToList<Ciclo>();
		}
	}
}