using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
	public partial class TipoEducacionEje
	{
		public static List<TipoEducacionEje> GetAll()
		{
			return
				(
				from query in Query.GetTipoEducacionEjes()
				select query
				).ToList<TipoEducacionEje>();
		}
	}
}