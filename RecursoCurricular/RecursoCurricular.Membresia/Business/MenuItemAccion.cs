using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.Membresia
{
	public partial class MenuItemAccion
	{
		public static MenuItemAccion Get(Guid aplicacionId, Guid menuId, Guid menuItemId, Int32 accionCodigo)
		{
			return Query.GetMenuItemAcciones().SingleOrDefault<MenuItemAccion>(x => x.AplicacionId == aplicacionId && x.MenuItemId == menuItemId && x.AccionCodigo == accionCodigo);
		}

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