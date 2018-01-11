using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
	public partial class AmbitoExperienciaAprendizaje
	{
		public static List<AmbitoExperienciaAprendizaje> GetAll()
		{
			return
				(
				from query in Query.GetAmbitoExperienciaAprendizajes()
				select query
				).ToList<AmbitoExperienciaAprendizaje>();
		}
	}
}