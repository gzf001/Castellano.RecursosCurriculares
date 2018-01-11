using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
	public partial class NucleoAprendizaje
	{
		public static List<NucleoAprendizaje> GetAll()
		{
			return
				(
				from query in Query.GetNucleoAprendizajes()
				select query
				).ToList<NucleoAprendizaje>();
		}
	}
}