using System;
using System.Linq;
namespace RecursoCurricular.RecursosCurriculares
{
	public partial class ObjetivoTransversal
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			ObjetivoTransversal objetivoTransversal = context.ObjetivoTransversales.SingleOrDefault<ObjetivoTransversal>(x => x == this);

			if (objetivoTransversal == null)
			{
				objetivoTransversal = new ObjetivoTransversal
				{
					AnoNumero = this.AnoNumero,
					TipoEducacionCodigo = this.TipoEducacionCodigo,
					GradoCodigo = this.GradoCodigo,
					SectorId = this.SectorId,
					UnidadId = this.UnidadId,
					Id = this.Id
				};

				context.ObjetivoTransversales.InsertOnSubmit(objetivoTransversal);
			}

			objetivoTransversal.Numero = this.Numero;
			objetivoTransversal.Descripcion = this.Descripcion;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			ObjetivoTransversal objetivoTransversal = context.ObjetivoTransversales.SingleOrDefault<ObjetivoTransversal>(x => x == this);

			if (objetivoTransversal != null)
			{
				context.ObjetivoTransversales.DeleteOnSubmit(objetivoTransversal);
			}
			PostDelete(context);
		}
	}
}