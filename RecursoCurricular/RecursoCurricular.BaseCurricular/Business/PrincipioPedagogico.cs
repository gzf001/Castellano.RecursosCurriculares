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

        public static List<PrincipioPedagogico> GetAll(RecursoCurricular.Anio anio)
        {
            return
                (
                from query in Query.GetPrincipioPedagogicos(anio)
                orderby query.AnoNumero
                select query
                ).ToList<PrincipioPedagogico>();
        }
    }
}