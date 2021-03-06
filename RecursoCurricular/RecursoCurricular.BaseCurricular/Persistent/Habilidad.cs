using System;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
	public partial class Habilidad
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			Habilidad habilidad = context.Habilidades.SingleOrDefault<Habilidad>(x => x == this);

			if (habilidad == null)
			{
				habilidad = new Habilidad
				{
					Id = this.Id,
					TipoEducacionCodigo = this.TipoEducacionCodigo,
					AnoNumero = this.AnoNumero,
					SectorId = this.SectorId
				};

				context.Habilidades.InsertOnSubmit(habilidad);
			}

			habilidad.Numero = this.Numero;
			habilidad.Descripcion = this.Descripcion;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			Habilidad habilidad = context.Habilidades.SingleOrDefault<Habilidad>(x => x == this);

			if (habilidad != null)
			{
				context.Habilidades.DeleteOnSubmit(habilidad);
			}
			PostDelete(context);
		}
	}
}