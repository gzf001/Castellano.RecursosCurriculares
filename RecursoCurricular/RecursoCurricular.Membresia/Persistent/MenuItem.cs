using System;
using System.Linq;
namespace RecursoCurricular.Membresia
{
	public partial class MenuItem
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			MenuItem menuItem = context.MenuItemes.SingleOrDefault<MenuItem>(x => x == this);

			if (menuItem == null)
			{
				menuItem = new MenuItem
				{
					AplicacionId = this.AplicacionId,
					Id = this.Id
				};

				context.MenuItemes.InsertOnSubmit(menuItem);
			}

			menuItem.MenuItemId = this.MenuItemId == default(Guid) ? null : this.MenuItemId;
			menuItem.Nombre = this.Nombre;
			menuItem.Informacion = this.Informacion;
			menuItem.Icono = this.Icono;
			menuItem.Url = this.Url;
			menuItem.Visible = this.Visible;
			menuItem.Orden = this.Orden;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			MenuItem menuItem = context.MenuItemes.SingleOrDefault<MenuItem>(x => x == this);

			if (menuItem != null)
			{
				context.MenuItemes.DeleteOnSubmit(menuItem);
			}
			PostDelete(context);
		}
	}
}