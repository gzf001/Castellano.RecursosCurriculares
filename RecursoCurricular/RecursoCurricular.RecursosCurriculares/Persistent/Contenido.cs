using System;
using System.Linq;
namespace RecursoCurricular.RecursosCurriculares
{
	public partial class Contenido
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			Contenido contenido = context.Contenidos.SingleOrDefault<Contenido>(x => x == this);

			if (contenido == null)
			{
				contenido = new Contenido
				{
					AnoNumero = this.AnoNumero,
					TipoEducacionCodigo = this.TipoEducacionCodigo,
					SectorId = this.SectorId,
					EjeId = this.EjeId,
					GradoCodigo = this.GradoCodigo,
					Id = this.Id
				};

				context.Contenidos.InsertOnSubmit(contenido);
			}

			contenido.Numero = this.Numero;
			contenido.Descripcion = this.Descripcion;
			contenido.Transversal = this.Transversal;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			Contenido contenido = context.Contenidos.SingleOrDefault<Contenido>(x => x == this);

			if (contenido != null)
			{
				context.Contenidos.DeleteOnSubmit(contenido);
			}
			PostDelete(context);
		}
	}
}