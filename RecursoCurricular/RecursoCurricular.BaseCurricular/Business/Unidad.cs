using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
	public partial class Unidad
	{
		public static List<Unidad> GetAll()
		{
			return
				(
				from query in Query.GetUnidades()
				select query
				).ToList<Unidad>();
		}
	}
}