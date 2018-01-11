using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.RecursosCurriculares
{
	public partial class ObjetivoTransversal
	{
		public static List<ObjetivoTransversal> GetAll()
		{
			return
				(
				from query in Query.GetObjetivoTransversales()
				select query
				).ToList<ObjetivoTransversal>();
		}
	}
}