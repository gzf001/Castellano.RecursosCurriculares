using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.RecursosCurriculares
{
	public partial class AprendizajeIndicador
	{
		public static List<AprendizajeIndicador> GetAll()
		{
			return
				(
				from query in Query.GetAprendizajeIndicadores()
				select query
				).ToList<AprendizajeIndicador>();
		}
	}
}