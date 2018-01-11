using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
	public partial class ObjetivoAprendizajeTransversal
	{
		public static List<ObjetivoAprendizajeTransversal> GetAll()
		{
			return
				(
				from query in Query.GetObjetivoAprendizajeTransversales()
				select query
				).ToList<ObjetivoAprendizajeTransversal>();
		}
	}
}