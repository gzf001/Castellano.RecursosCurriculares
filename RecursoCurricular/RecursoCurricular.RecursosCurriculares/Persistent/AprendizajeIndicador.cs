using System;
using System.Linq;
namespace RecursoCurricular.RecursosCurriculares
{
	public partial class AprendizajeIndicador
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			AprendizajeIndicador aprendizajeIndicador = context.AprendizajeIndicadores.SingleOrDefault<AprendizajeIndicador>(x => x == this);

			if (aprendizajeIndicador == null)
			{
				aprendizajeIndicador = new AprendizajeIndicador
				{
					AnoNumero = this.AnoNumero,
					TipoEducacionCodigo = this.TipoEducacionCodigo,
					GradoCodigo = this.GradoCodigo,
					SectorId = this.SectorId,
					AprendizajeId = this.AprendizajeId,
					Id = this.Id
				};

				context.AprendizajeIndicadores.InsertOnSubmit(aprendizajeIndicador);
			}

			aprendizajeIndicador.CategoriaCodigo = this.CategoriaCodigo == default(Int32) ? null : this.CategoriaCodigo;
			aprendizajeIndicador.Numero = this.Numero;
			aprendizajeIndicador.Descripcion = this.Descripcion;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			AprendizajeIndicador aprendizajeIndicador = context.AprendizajeIndicadores.SingleOrDefault<AprendizajeIndicador>(x => x == this);

			if (aprendizajeIndicador != null)
			{
				context.AprendizajeIndicadores.DeleteOnSubmit(aprendizajeIndicador);
			}
			PostDelete(context);
		}
	}
}