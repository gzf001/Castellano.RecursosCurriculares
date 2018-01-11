using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
	public partial class Habilidad
	{
		public static List<Habilidad> GetAll()
		{
			return
				(
				from query in Query.GetHabilidades()
				select query
				).ToList<Habilidad>();
		}
	}
}