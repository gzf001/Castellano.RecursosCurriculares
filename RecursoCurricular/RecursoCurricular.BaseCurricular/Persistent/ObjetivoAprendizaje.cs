using System;
using System.Linq;
using RecursoCurricular.Educacion;

namespace RecursoCurricular.BaseCurricular
{
	public partial class ObjetivoAprendizaje
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			ObjetivoAprendizaje objetivoAprendizaje = context.ObjetivoAprendizajes.SingleOrDefault<ObjetivoAprendizaje>(x => x == this);

			if (objetivoAprendizaje == null)
			{
				objetivoAprendizaje = new ObjetivoAprendizaje
				{
					TipoEducacionCodigo = this.TipoEducacionCodigo,
					AnoNumero = this.AnoNumero,
					GradoCodigo = this.GradoCodigo,
					SectorId = this.SectorId,
					EjeId = this.EjeId,
					Id = this.Id
				};

				context.ObjetivoAprendizajes.InsertOnSubmit(objetivoAprendizaje);
			}

			objetivoAprendizaje.Numero = this.Numero;
			objetivoAprendizaje.Descripcion = this.Descripcion;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			ObjetivoAprendizaje objetivoAprendizaje = context.ObjetivoAprendizajes.SingleOrDefault<ObjetivoAprendizaje>(x => x == this);

			if (objetivoAprendizaje != null)
			{
				context.ObjetivoAprendizajes.DeleteOnSubmit(objetivoAprendizaje);
			}
			PostDelete(context);
		}
    }
}