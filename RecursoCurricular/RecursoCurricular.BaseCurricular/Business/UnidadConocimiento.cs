using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
	public partial class UnidadConocimiento
	{
		public static List<UnidadConocimiento> GetAll()
		{
			return
				(
				from query in Query.GetUnidadConocimientos()
				select query
				).ToList<UnidadConocimiento>();
		}
	}
}