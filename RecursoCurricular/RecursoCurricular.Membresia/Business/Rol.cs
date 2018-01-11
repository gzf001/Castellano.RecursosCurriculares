using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.Membresia
{
	public partial class Rol
	{
		public static List<Rol> GetAll()
		{
			return
				(
				from query in Query.GetRoles()
				select query
				).ToList<Rol>();
		}
	}
}