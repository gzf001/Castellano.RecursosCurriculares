using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.RecursosCurriculares
{
	public partial class UnidadAprendizaje
	{
		public static List<UnidadAprendizaje> GetAll()
		{
			return
				(
				from query in Query.GetUnidadAprendizajes()
				select query
				).ToList<UnidadAprendizaje>();
		}
	}
}