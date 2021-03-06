using System;
using System.Linq;
namespace RecursoCurricular.RecursosCurriculares
{
	public partial class Aprendizaje
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			Aprendizaje aprendizaje = context.Aprendizajes.SingleOrDefault<Aprendizaje>(x => x == this);

			if (aprendizaje == null)
			{
				aprendizaje = new Aprendizaje
				{
					AnoNumero = this.AnoNumero,
					TipoEducacionCodigo = this.TipoEducacionCodigo,
					GradoCodigo = this.GradoCodigo,
					SectorId = this.SectorId,
					Id = this.Id
				};

				context.Aprendizajes.InsertOnSubmit(aprendizaje);
			}

			aprendizaje.Numero = this.Numero;
			aprendizaje.Descripcion = this.Descripcion;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			Aprendizaje aprendizaje = context.Aprendizajes.SingleOrDefault<Aprendizaje>(x => x == this);

			if (aprendizaje != null)
			{
				context.Aprendizajes.DeleteOnSubmit(aprendizaje);
			}
			PostDelete(context);
		}
	}
}