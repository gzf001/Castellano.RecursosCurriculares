using System;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
	public partial class UnidadIndicador
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			UnidadIndicador unidadIndicador = context.UnidadIndicadores.SingleOrDefault<UnidadIndicador>(x => x == this);

			if (unidadIndicador == null)
			{
				unidadIndicador = new UnidadIndicador
				{
					TipoEducacionCodigo = this.TipoEducacionCodigo,
					AnoNumero = this.AnoNumero,
					GradoCodigo = this.GradoCodigo,
					SectorId = this.SectorId,
					UnidadId = this.UnidadId,
					EjeId = this.EjeId,
					ObjetivoAprendizajeId = this.ObjetivoAprendizajeId,
					IndicadorId = this.IndicadorId
				};

				context.UnidadIndicadores.InsertOnSubmit(unidadIndicador);
			}

			unidadIndicador.Orden = this.Orden;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			UnidadIndicador unidadIndicador = context.UnidadIndicadores.SingleOrDefault<UnidadIndicador>(x => x == this);

			if (unidadIndicador != null)
			{
				context.UnidadIndicadores.DeleteOnSubmit(unidadIndicador);
			}
			PostDelete(context);
		}
	}
}