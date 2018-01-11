using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.Membresia
{
	public partial class Aplicacion
	{
		public static List<Aplicacion> GetAll()
		{
			return
				(
				from query in Query.GetAplicaciones()
				select query
				).ToList<Aplicacion>();
		}
	}
}