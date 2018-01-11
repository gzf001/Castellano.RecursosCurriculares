using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular
{
	public partial class EstadoCivil
	{
		public static List<EstadoCivil> GetAll()
		{
			return
				(
				from query in Query.GetEstadoCiviles()
				select query
				).ToList<EstadoCivil>();
		}
	}
}