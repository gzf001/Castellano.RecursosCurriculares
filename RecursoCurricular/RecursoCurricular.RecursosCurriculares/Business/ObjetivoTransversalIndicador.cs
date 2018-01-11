using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.RecursosCurriculares
{
	public partial class ObjetivoTransversalIndicador
	{
		public static List<ObjetivoTransversalIndicador> GetAll()
		{
			return
				(
				from query in Query.GetObjetivoTransversalIndicadores()
				select query
				).ToList<ObjetivoTransversalIndicador>();
		}
	}
}