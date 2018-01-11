using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular
{
	public partial class Persona
	{
		public static List<Persona> GetAll()
		{
			return
				(
				from query in Query.GetPersonas()
				select query
				).ToList<Persona>();
		}
	}
}