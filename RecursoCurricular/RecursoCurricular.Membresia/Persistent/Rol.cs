using System;
using System.Linq;
namespace RecursoCurricular.Membresia
{
	public partial class Rol
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			Rol rol = context.Roles.SingleOrDefault<Rol>(x => x == this);

			if (rol == null)
			{
				rol = new Rol
				{
					Id = this.Id
				};

				context.Roles.InsertOnSubmit(rol);
			}

			rol.Nombre = this.Nombre;
			rol.Clave = this.Clave;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			Rol rol = context.Roles.SingleOrDefault<Rol>(x => x == this);

			if (rol != null)
			{
				context.Roles.DeleteOnSubmit(rol);
			}
			PostDelete(context);
		}
	}
}