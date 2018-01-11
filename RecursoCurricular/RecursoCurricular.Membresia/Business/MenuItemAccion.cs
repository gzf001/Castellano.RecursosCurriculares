using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.Membresia
{
	public partial class MenuItemAccion
	{
		public static List<MenuItemAccion> GetAll()
		{
			return
				(
				from query in Query.GetMenuItemAcciones()
				select query
				).ToList<MenuItemAccion>();
		}
	}
}