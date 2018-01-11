using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
	public partial class DimensionOAT
	{
		public static List<DimensionOAT> GetAll()
		{
			return
				(
				from query in Query.GetDimensionOATes()
				select query
				).ToList<DimensionOAT>();
		}
	}
}