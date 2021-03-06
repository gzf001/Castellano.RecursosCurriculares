using System;
using System.Linq;
namespace RecursoCurricular.RecursosCurriculares
{
	public partial class AprendizajeContenido
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			AprendizajeContenido aprendizajeContenido = context.AprendizajeContenidos.SingleOrDefault<AprendizajeContenido>(x => x == this);

			if (aprendizajeContenido == null)
			{
				aprendizajeContenido = new AprendizajeContenido
				{
					AnoNumero = this.AnoNumero,
					TipoEducacionCodigo = this.TipoEducacionCodigo,
					GradoCodigo = this.GradoCodigo,
					SectorId = this.SectorId,
					AprendizajeId = this.AprendizajeId,
					EjeId = this.EjeId,
					ContenidoId = this.ContenidoId
				};

				context.AprendizajeContenidos.InsertOnSubmit(aprendizajeContenido);
			}

			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			AprendizajeContenido aprendizajeContenido = context.AprendizajeContenidos.SingleOrDefault<AprendizajeContenido>(x => x == this);

			if (aprendizajeContenido != null)
			{
				context.AprendizajeContenidos.DeleteOnSubmit(aprendizajeContenido);
			}
			PostDelete(context);
		}
	}
}