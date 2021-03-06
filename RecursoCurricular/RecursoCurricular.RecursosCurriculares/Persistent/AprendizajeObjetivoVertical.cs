using System;
using System.Linq;
namespace RecursoCurricular.RecursosCurriculares
{
	public partial class AprendizajeObjetivoVertical
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			AprendizajeObjetivoVertical aprendizajeObjetivoVertical = context.AprendizajeObjetivoVerticales.SingleOrDefault<AprendizajeObjetivoVertical>(x => x == this);

			if (aprendizajeObjetivoVertical == null)
			{
				aprendizajeObjetivoVertical = new AprendizajeObjetivoVertical
				{
					AnoNumero = this.AnoNumero,
					TipoEducacionCodigo = this.TipoEducacionCodigo,
					GradoCodigo = this.GradoCodigo,
					SectorId = this.SectorId,
					AprendizajeId = this.AprendizajeId,
					ObjetivoVerticalId = this.ObjetivoVerticalId
				};

				context.AprendizajeObjetivoVerticales.InsertOnSubmit(aprendizajeObjetivoVertical);
			}

			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			AprendizajeObjetivoVertical aprendizajeObjetivoVertical = context.AprendizajeObjetivoVerticales.SingleOrDefault<AprendizajeObjetivoVertical>(x => x == this);

			if (aprendizajeObjetivoVertical != null)
			{
				context.AprendizajeObjetivoVerticales.DeleteOnSubmit(aprendizajeObjetivoVertical);
			}
			PostDelete(context);
		}
	}
}