using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RecursoCurricular.BaseCurricular
{
	public partial class Eje
	{
        public static List<Eje> GetAll()
		{
			return
				(
				from query in Query.GetEjes()
				select query
				).ToList<Eje>();
		}
    }
}