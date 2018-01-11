using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular
{
	public partial class Sexo
	{
		public static List<Sexo> GetAll()
		{
			return
				(
				from query in Query.GetSexos()
				select query
				).ToList<Sexo>();
		}
	}
}