using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular
{
	public partial class Comuna
	{
		public static List<Comuna> GetAll()
		{
			return
				(
				from query in Query.GetComunas()
				select query
				).ToList<Comuna>();
		}
	}
}