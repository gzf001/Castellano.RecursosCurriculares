using System;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
	public partial class UnidadActitud
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			UnidadActitud unidadActitud = context.UnidadActitudes.SingleOrDefault<UnidadActitud>(x => x == this);

			if (unidadActitud == null)
			{
				unidadActitud = new UnidadActitud
				{
					TipoEducacionCodigo = this.TipoEducacionCodigo,
					AnoNumero = this.AnoNumero,
					GradoCodigo = this.GradoCodigo,
					SectorId = this.SectorId,
					UnidadId = this.UnidadId,
					ActitudId = this.ActitudId
				};

				context.UnidadActitudes.InsertOnSubmit(unidadActitud);
			}

			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			UnidadActitud unidadActitud = context.UnidadActitudes.SingleOrDefault<UnidadActitud>(x => x == this);

			if (unidadActitud != null)
			{
				context.UnidadActitudes.DeleteOnSubmit(unidadActitud);
			}
			PostDelete(context);
		}
	}
}