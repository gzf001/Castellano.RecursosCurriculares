using System;
using System.Linq;
namespace RecursoCurricular.RecursosCurriculares
{
	public partial class UnidadAprendizaje
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			UnidadAprendizaje unidadAprendizaje = context.UnidadAprendizajes.SingleOrDefault<UnidadAprendizaje>(x => x == this);

			if (unidadAprendizaje == null)
			{
				unidadAprendizaje = new UnidadAprendizaje
				{
					AnoNumero = this.AnoNumero,
					TipoEducacionCodigo = this.TipoEducacionCodigo,
					GradoCodigo = this.GradoCodigo,
					SectorId = this.SectorId,
					UnidadId = this.UnidadId,
					AprendizajeId = this.AprendizajeId
				};

				context.UnidadAprendizajes.InsertOnSubmit(unidadAprendizaje);
			}

			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			UnidadAprendizaje unidadAprendizaje = context.UnidadAprendizajes.SingleOrDefault<UnidadAprendizaje>(x => x == this);

			if (unidadAprendizaje != null)
			{
				context.UnidadAprendizajes.DeleteOnSubmit(unidadAprendizaje);
			}
			PostDelete(context);
		}
	}
}