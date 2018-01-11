using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
	public partial class UnidadActitud
	{
		public static List<UnidadActitud> GetAll()
		{
			return
				(
				from query in Query.GetUnidadActitudes()
				select query
				).ToList<UnidadActitud>();
		}
	}
}