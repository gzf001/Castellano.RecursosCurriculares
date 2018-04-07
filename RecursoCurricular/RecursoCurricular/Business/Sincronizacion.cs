using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular
{
	public partial class Sincronizacion
	{
		public static List<Sincronizacion> GetAll()
		{
			return
				(
				from query in Query.GetSincronizaciones()
				select query
				).ToList<Sincronizacion>();
		}
	}
}