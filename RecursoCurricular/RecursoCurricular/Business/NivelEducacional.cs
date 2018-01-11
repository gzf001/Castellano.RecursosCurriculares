using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular
{
	public partial class NivelEducacional
	{
		public static List<NivelEducacional> GetAll()
		{
			return
				(
				from query in Query.GetNivelEducacionales()
				select query
				).ToList<NivelEducacional>();
		}
	}
}