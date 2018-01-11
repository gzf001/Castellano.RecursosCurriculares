using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.Membresia
{
	public partial class PerfilUsuario
	{
		public static List<PerfilUsuario> GetAll()
		{
			return
				(
				from query in Query.GetPerfilUsuarios()
				select query
				).ToList<PerfilUsuario>();
		}
	}
}