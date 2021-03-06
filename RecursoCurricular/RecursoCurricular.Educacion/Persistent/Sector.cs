using System;
using System.Linq;
namespace RecursoCurricular.Educacion
{
	public partial class Sector
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			Sector sector = context.Sectores.SingleOrDefault<Sector>(x => x == this);

			if (sector == null)
			{
				sector = new Sector
				{
					Id = this.Id
				};

				context.Sectores.InsertOnSubmit(sector);
			}

			sector.Nombre = this.Nombre;
			sector.Orden = this.Orden;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			Sector sector = context.Sectores.SingleOrDefault<Sector>(x => x == this);

			if (sector != null)
			{
				context.Sectores.DeleteOnSubmit(sector);
			}
			PostDelete(context);
		}
	}
}