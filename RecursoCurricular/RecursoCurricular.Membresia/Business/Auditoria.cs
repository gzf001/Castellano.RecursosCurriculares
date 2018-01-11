using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.Membresia
{
	public partial class Auditoria
	{
		public static List<Auditoria> GetAll()
		{
			return
				(
				from query in Query.GetAuditorias()
				select query
				).ToList<Auditoria>();
		}
	}
}