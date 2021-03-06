using System;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
	public partial class TipoEducacionEje
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			TipoEducacionEje tipoEducacionEje = context.TipoEducacionEjes.SingleOrDefault<TipoEducacionEje>(x => x == this);

			if (tipoEducacionEje == null)
			{
				tipoEducacionEje = new TipoEducacionEje
				{
					TipoEducacionCodigo = this.TipoEducacionCodigo,
					AnoNumero = this.AnoNumero,
					SectorId = this.SectorId,
					EjeId = this.EjeId
				};

				context.TipoEducacionEjes.InsertOnSubmit(tipoEducacionEje);
			}

			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			TipoEducacionEje tipoEducacionEje = context.TipoEducacionEjes.SingleOrDefault<TipoEducacionEje>(x => x == this);

			if (tipoEducacionEje != null)
			{
				context.TipoEducacionEjes.DeleteOnSubmit(tipoEducacionEje);
			}
			PostDelete(context);
		}
	}
}