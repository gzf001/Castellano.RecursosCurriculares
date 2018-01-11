using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
	public partial class UnidadSubHabilidad
	{
		public static List<UnidadSubHabilidad> GetAll()
		{
			return
				(
				from query in Query.GetUnidadSubHabilidades()
				select query
				).ToList<UnidadSubHabilidad>();
		}
	}
}