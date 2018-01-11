using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular
{
	public partial class Ciudad
	{
		public static List<Ciudad> GetAll()
		{
			return
				(
				from query in Query.GetCiudades()
				select query
				).ToList<Ciudad>();
		}
	}
}