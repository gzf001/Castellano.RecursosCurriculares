using System;
using System.Linq;
namespace RecursoCurricular.Membresia
{
	public partial class MenuItemAccion
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			MenuItemAccion menuItemAccion = context.MenuItemAcciones.SingleOrDefault<MenuItemAccion>(x => x == this);

			if (menuItemAccion == null)
			{
				menuItemAccion = new MenuItemAccion
				{
					AplicacionId = this.AplicacionId,
					MenuItemId = this.MenuItemId,
					AccionCodigo = this.AccionCodigo
				};

				context.MenuItemAcciones.InsertOnSubmit(menuItemAccion);
			}

			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			MenuItemAccion menuItemAccion = context.MenuItemAcciones.SingleOrDefault<MenuItemAccion>(x => x == this);

			if (menuItemAccion != null)
			{
				context.MenuItemAcciones.DeleteOnSubmit(menuItemAccion);
			}
			PostDelete(context);
		}
	}
}