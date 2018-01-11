using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.Membresia
{
	public partial class RolPersona
	{
		public static List<RolPersona> GetAll()
		{
			return
				(
				from query in Query.GetRolPersonas()
				select query
				).ToList<RolPersona>();
		}
	}
}