using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
	public partial class Conocimiento
	{
		public static List<Conocimiento> GetAll()
		{
			return
				(
				from query in Query.GetConocimientos()
				select query
				).ToList<Conocimiento>();
		}
	}
}