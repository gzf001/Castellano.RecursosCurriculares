using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.RecursosCurriculares
{
	public partial class AprendizajeContenido
	{
		public static List<AprendizajeContenido> GetAll()
		{
			return
				(
				from query in Query.GetAprendizajeContenidos()
				select query
				).ToList<AprendizajeContenido>();
		}
	}
}