using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.RecursosCurriculares
{
	public partial class Eje
	{
		public static List<Eje> GetAll()
		{
			return
				(
				from query in Query.GetEjes()
				select query
				).ToList<Eje>();
		}
	}
}