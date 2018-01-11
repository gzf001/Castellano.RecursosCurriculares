using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.Membresia
{
	public partial class Perfil
	{
		public static List<Perfil> GetAll()
		{
			return
				(
				from query in Query.GetPerfiles()
				select query
				).ToList<Perfil>();
		}
	}
}