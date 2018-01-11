using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.RecursosCurriculares
{
	public partial class ObjetivoVertical
	{
		public static List<ObjetivoVertical> GetAll()
		{
			return
				(
				from query in Query.GetObjetivoVerticales()
				select query
				).ToList<ObjetivoVertical>();
		}
	}
}