using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
	public partial class ObjetivoAprendizaje
	{
		public static List<ObjetivoAprendizaje> GetAll()
		{
			return
				(
				from query in Query.GetObjetivoAprendizajes()
				select query
				).ToList<ObjetivoAprendizaje>();
		}
	}
}