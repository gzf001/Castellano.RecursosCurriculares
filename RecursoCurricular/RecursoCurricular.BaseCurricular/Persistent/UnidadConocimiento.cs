using System;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
	public partial class UnidadConocimiento
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			UnidadConocimiento unidadConocimiento = context.UnidadConocimientos.SingleOrDefault<UnidadConocimiento>(x => x == this);

			if (unidadConocimiento == null)
			{
				unidadConocimiento = new UnidadConocimiento
				{
					TipoEducacionCodigo = this.TipoEducacionCodigo,
					AnoNumero = this.AnoNumero,
					GradoCodigo = this.GradoCodigo,
					SectorId = this.SectorId,
					UnidadId = this.UnidadId,
					ConocimientoId = this.ConocimientoId
				};

				context.UnidadConocimientos.InsertOnSubmit(unidadConocimiento);
			}

			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			UnidadConocimiento unidadConocimiento = context.UnidadConocimientos.SingleOrDefault<UnidadConocimiento>(x => x == this);

			if (unidadConocimiento != null)
			{
				context.UnidadConocimientos.DeleteOnSubmit(unidadConocimiento);
			}
			PostDelete(context);
		}
	}
}