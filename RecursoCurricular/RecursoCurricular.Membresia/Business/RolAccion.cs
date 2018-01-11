using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.Membresia
{
	public partial class RolAccion
	{
		public static List<RolAccion> GetAll()
		{
			return
				(
				from query in Query.GetRolAcciones()
				select query
				).ToList<RolAccion>();
		}
	}
}