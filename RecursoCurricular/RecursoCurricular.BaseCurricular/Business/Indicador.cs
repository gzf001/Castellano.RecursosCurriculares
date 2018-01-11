using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
	public partial class Indicador
	{
		public static List<Indicador> GetAll()
		{
			return
				(
				from query in Query.GetIndicadores()
				select query
				).ToList<Indicador>();
		}
	}
}