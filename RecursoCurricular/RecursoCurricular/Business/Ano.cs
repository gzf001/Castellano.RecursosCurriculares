using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular
{
	public partial class Ano
	{
		public static List<Ano> GetAll()
		{
			return
				(
				from query in Query.GetAnos()
				select query
				).ToList<Ano>();
		}
	}
}