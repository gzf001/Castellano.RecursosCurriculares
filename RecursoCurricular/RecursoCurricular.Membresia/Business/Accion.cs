using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.Membresia
{
	public partial class Accion
	{
		public static List<Accion> GetAll()
		{
			return
				(
				from query in Query.GetAcciones()
				select query
				).ToList<Accion>();
		}
	}
}