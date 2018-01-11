using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
	public partial class EjeParvulo
	{
		public static List<EjeParvulo> GetAll()
		{
			return
				(
				from query in Query.GetEjeParvulos()
				select query
				).ToList<EjeParvulo>();
		}
	}
}