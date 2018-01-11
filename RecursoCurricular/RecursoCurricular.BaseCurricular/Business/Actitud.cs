using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
	public partial class Actitud
	{
		public static List<Actitud> GetAll()
		{
			return
				(
				from query in Query.GetActitudes()
				select query
				).ToList<Actitud>();
		}
	}
}