using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
	public partial class UnidadObjetivoAprendizaje
	{
		public static List<UnidadObjetivoAprendizaje> GetAll()
		{
			return
				(
				from query in Query.GetUnidadObjetivoAprendizajes()
				select query
				).ToList<UnidadObjetivoAprendizaje>();
		}
	}
}