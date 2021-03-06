using System;
using System.Linq;
namespace RecursoCurricular.RecursosCurriculares
{
	public partial class ObjetivoVertical
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			ObjetivoVertical objetivoVertical = context.ObjetivoVerticales.SingleOrDefault<ObjetivoVertical>(x => x == this);

			if (objetivoVertical == null)
			{
				objetivoVertical = new ObjetivoVertical
				{
					AnoNumero = this.AnoNumero,
					TipoEducacionCodigo = this.TipoEducacionCodigo,
					GradoCodigo = this.GradoCodigo,
					SectorId = this.SectorId,
					Id = this.Id
				};

				context.ObjetivoVerticales.InsertOnSubmit(objetivoVertical);
			}

			objetivoVertical.Numero = this.Numero;
			objetivoVertical.Descripcion = this.Descripcion;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			ObjetivoVertical objetivoVertical = context.ObjetivoVerticales.SingleOrDefault<ObjetivoVertical>(x => x == this);

			if (objetivoVertical != null)
			{
				context.ObjetivoVerticales.DeleteOnSubmit(objetivoVertical);
			}
			PostDelete(context);
		}
	}
}