using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.RecursosCurriculares
{
	public partial class Contenido
	{
		public static List<Contenido> GetAll()
		{
			return
				(
				from query in Query.GetContenidos()
				select query
				).ToList<Contenido>();
		}
	}
}