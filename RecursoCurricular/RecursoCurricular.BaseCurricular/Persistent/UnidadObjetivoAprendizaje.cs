using System;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
	public partial class UnidadObjetivoAprendizaje
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			UnidadObjetivoAprendizaje unidadObjetivoAprendizaje = context.UnidadObjetivoAprendizajes.SingleOrDefault<UnidadObjetivoAprendizaje>(x => x == this);

			if (unidadObjetivoAprendizaje == null)
			{
				unidadObjetivoAprendizaje = new UnidadObjetivoAprendizaje
				{
					TipoEducacionCodigo = this.TipoEducacionCodigo,
					AnoNumero = this.AnoNumero,
					GradoCodigo = this.GradoCodigo,
					SectorId = this.SectorId,
					UnidadId = this.UnidadId,
					EjeId = this.EjeId,
					ObjetivoAprendizajeId = this.ObjetivoAprendizajeId
				};

				context.UnidadObjetivoAprendizajes.InsertOnSubmit(unidadObjetivoAprendizaje);
			}

			unidadObjetivoAprendizaje.Orden = this.Orden;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			UnidadObjetivoAprendizaje unidadObjetivoAprendizaje = context.UnidadObjetivoAprendizajes.SingleOrDefault<UnidadObjetivoAprendizaje>(x => x == this);

			if (unidadObjetivoAprendizaje != null)
			{
				context.UnidadObjetivoAprendizajes.DeleteOnSubmit(unidadObjetivoAprendizaje);
			}
			PostDelete(context);
		}
    }
}