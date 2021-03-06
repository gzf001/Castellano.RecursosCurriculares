using System;
using System.Linq;
namespace RecursoCurricular
{
	public partial class Sistema
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			Sistema sistema = context.Sistemas.SingleOrDefault<Sistema>(x => x == this);

			if (sistema == null)
			{
				sistema = new Sistema
				{
					Id = this.Id
				};

				context.Sistemas.InsertOnSubmit(sistema);
			}

			sistema.Nombre = this.Nombre;
			sistema.Url = this.Url;
			sistema.Activo = this.Activo;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			Sistema sistema = context.Sistemas.SingleOrDefault<Sistema>(x => x == this);

			if (sistema != null)
			{
				context.Sistemas.DeleteOnSubmit(sistema);
			}
			PostDelete(context);
		}
	}
}