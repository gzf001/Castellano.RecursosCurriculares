using System;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
	public partial class Actitud
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			Actitud actitud = context.Actitudes.SingleOrDefault<Actitud>(x => x == this);

			if (actitud == null)
			{
				actitud = new Actitud
				{
					TipoEducacionCodigo = this.TipoEducacionCodigo,
					AnoNumero = this.AnoNumero,
					SectorId = this.SectorId,
					Id = this.Id
				};

				context.Actitudes.InsertOnSubmit(actitud);
			}

			actitud.Numero = this.Numero;
			actitud.Nombre = this.Nombre;
			actitud.Descripcion = this.Descripcion;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			Actitud actitud = context.Actitudes.SingleOrDefault<Actitud>(x => x == this);

			if (actitud != null)
			{
				context.Actitudes.DeleteOnSubmit(actitud);
			}
			PostDelete(context);
		}
	}
}