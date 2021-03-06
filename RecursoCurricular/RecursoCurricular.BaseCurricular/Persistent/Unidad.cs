using System;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
	public partial class Unidad
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			Unidad unidad = context.Unidades.SingleOrDefault<Unidad>(x => x == this);

			if (unidad == null)
			{
				unidad = new Unidad
				{
					TipoEducacionCodigo = this.TipoEducacionCodigo,
					AnoNumero = this.AnoNumero,
					GradoCodigo = this.GradoCodigo,
					SectorId = this.SectorId,
					Id = this.Id
				};

				context.Unidades.InsertOnSubmit(unidad);
			}

			unidad.Proposito = this.Proposito;
			unidad.ConocimientoPrevio = this.ConocimientoPrevio;
			unidad.PalabraClave = this.PalabraClave;
			unidad.Numero = this.Numero;
			unidad.Nombre = this.Nombre;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			Unidad unidad = context.Unidades.SingleOrDefault<Unidad>(x => x == this);

			if (unidad != null)
			{
				context.Unidades.DeleteOnSubmit(unidad);
			}
			PostDelete(context);
		}
	}
}