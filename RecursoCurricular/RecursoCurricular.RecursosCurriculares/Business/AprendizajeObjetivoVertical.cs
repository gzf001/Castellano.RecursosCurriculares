using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.RecursosCurriculares
{
	public partial class AprendizajeObjetivoVertical
	{
		public static List<AprendizajeObjetivoVertical> GetAll()
		{
			return
				(
				from query in Query.GetAprendizajeObjetivoVerticales()
				select query
				).ToList<AprendizajeObjetivoVertical>();
		}
	}
}