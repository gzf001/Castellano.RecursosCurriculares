using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
	public partial class UnidadIndicador
	{
		public static List<UnidadIndicador> GetAll()
		{
			return
				(
				from query in Query.GetUnidadIndicadores()
				select query
				).ToList<UnidadIndicador>();
		}
	}
}