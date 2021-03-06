using System;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
	public partial class Indicador
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			Indicador indicador = context.Indicadores.SingleOrDefault<Indicador>(x => x == this);

			if (indicador == null)
			{
				indicador = new Indicador
				{
					TipoEducacionCodigo = this.TipoEducacionCodigo,
					AnoNumero = this.AnoNumero,
					GradoCodigo = this.GradoCodigo,
					SectorId = this.SectorId,
					EjeId = this.EjeId,
					ObjetivoAprendizajeId = this.ObjetivoAprendizajeId,
					Id = this.Id
				};

				context.Indicadores.InsertOnSubmit(indicador);
			}

			indicador.Numero = this.Numero;
			indicador.Descripcion = this.Descripcion;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			Indicador indicador = context.Indicadores.SingleOrDefault<Indicador>(x => x == this);

			if (indicador != null)
			{
				context.Indicadores.DeleteOnSubmit(indicador);
			}
			PostDelete(context);
		}
	}
}