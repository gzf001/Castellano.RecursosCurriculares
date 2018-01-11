using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.Membresia
{
	public partial class Usuario
	{
		public static List<Usuario> GetAll()
		{
			return
				(
				from query in Query.GetUsuarios()
				select query
				).ToList<Usuario>();
		}
	}
}