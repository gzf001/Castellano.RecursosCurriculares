using System;
using System.Linq;
namespace RecursoCurricular.RecursosCurriculares
{
	public partial class ObjetivoTransversalIndicador
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			ObjetivoTransversalIndicador objetivoTransversalIndicador = context.ObjetivoTransversalIndicadores.SingleOrDefault<ObjetivoTransversalIndicador>(x => x == this);

			if (objetivoTransversalIndicador == null)
			{
				objetivoTransversalIndicador = new ObjetivoTransversalIndicador
				{
					AnoNumero = this.AnoNumero,
					TipoEducacionCodigo = this.TipoEducacionCodigo,
					GradoCodigo = this.GradoCodigo,
					SectorId = this.SectorId,
					UnidadId = this.UnidadId,
					ObjetivoTransversalId = this.ObjetivoTransversalId,
					Id = this.Id
				};

				context.ObjetivoTransversalIndicadores.InsertOnSubmit(objetivoTransversalIndicador);
			}

			objetivoTransversalIndicador.Numero = this.Numero;
			objetivoTransversalIndicador.Descripcion = this.Descripcion;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			ObjetivoTransversalIndicador objetivoTransversalIndicador = context.ObjetivoTransversalIndicadores.SingleOrDefault<ObjetivoTransversalIndicador>(x => x == this);

			if (objetivoTransversalIndicador != null)
			{
				context.ObjetivoTransversalIndicadores.DeleteOnSubmit(objetivoTransversalIndicador);
			}
			PostDelete(context);
		}
	}
}