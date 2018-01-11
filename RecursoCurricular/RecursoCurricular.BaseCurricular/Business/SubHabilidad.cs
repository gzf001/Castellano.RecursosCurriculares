using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
	public partial class SubHabilidad
	{
		public static List<SubHabilidad> GetAll()
		{
			return
				(
				from query in Query.GetSubHabilidades()
				select query
				).ToList<SubHabilidad>();
		}
	}
}