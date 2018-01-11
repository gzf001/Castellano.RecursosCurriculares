using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.RecursosCurriculares
{
	public partial class Aprendizaje
	{
		public static List<Aprendizaje> GetAll()
		{
			return
				(
				from query in Query.GetAprendizajes()
				select query
				).ToList<Aprendizaje>();
		}
	}
}