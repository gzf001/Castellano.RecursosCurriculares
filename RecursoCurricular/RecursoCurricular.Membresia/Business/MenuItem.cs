using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.Membresia
{
	public partial class MenuItem
	{
		public static List<MenuItem> GetAll()
		{
			return
				(
				from query in Query.GetMenuItemes()
				select query
				).ToList<MenuItem>();
		}
	}
}