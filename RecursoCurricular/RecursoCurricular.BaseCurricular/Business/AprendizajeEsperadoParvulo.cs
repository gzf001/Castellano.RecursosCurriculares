using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
	public partial class AprendizajeEsperadoParvulo
	{
		public static List<AprendizajeEsperadoParvulo> GetAll()
		{
			return
				(
				from query in Query.GetAprendizajeEsperadoParvulos()
				select query
				).ToList<AprendizajeEsperadoParvulo>();
		}
	}
}