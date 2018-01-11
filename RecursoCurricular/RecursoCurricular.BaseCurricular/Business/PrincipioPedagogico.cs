using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
	public partial class PrincipioPedagogico
	{
		public static List<PrincipioPedagogico> GetAll()
		{
			return
				(
				from query in Query.GetPrincipioPedagogicos()
				select query
				).ToList<PrincipioPedagogico>();
		}
	}
}