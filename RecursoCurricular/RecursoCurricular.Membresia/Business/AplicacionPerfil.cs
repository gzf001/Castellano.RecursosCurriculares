using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.Membresia
{
	public partial class AplicacionPerfil
	{
		public static List<AplicacionPerfil> GetAll()
		{
			return
				(
				from query in Query.GetAplicacionPerfiles()
				select query
				).ToList<AplicacionPerfil>();
		}
	}
}