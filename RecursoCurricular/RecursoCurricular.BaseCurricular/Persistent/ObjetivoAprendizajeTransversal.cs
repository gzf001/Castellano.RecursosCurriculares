using System;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
	public partial class ObjetivoAprendizajeTransversal
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			ObjetivoAprendizajeTransversal objetivoAprendizajeTransversal = context.ObjetivoAprendizajeTransversales.SingleOrDefault<ObjetivoAprendizajeTransversal>(x => x == this);

			if (objetivoAprendizajeTransversal == null)
			{
				objetivoAprendizajeTransversal = new ObjetivoAprendizajeTransversal
				{
					DimensionOATId = this.DimensionOATId,
					AnoNumero = this.AnoNumero,
					Id = this.Id
				};

				context.ObjetivoAprendizajeTransversales.InsertOnSubmit(objetivoAprendizajeTransversal);
			}

			objetivoAprendizajeTransversal.Numero = this.Numero;
			objetivoAprendizajeTransversal.Nombre = this.Nombre;
			objetivoAprendizajeTransversal.Descripcion = this.Descripcion;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			ObjetivoAprendizajeTransversal objetivoAprendizajeTransversal = context.ObjetivoAprendizajeTransversales.SingleOrDefault<ObjetivoAprendizajeTransversal>(x => x == this);

			if (objetivoAprendizajeTransversal != null)
			{
				context.ObjetivoAprendizajeTransversales.DeleteOnSubmit(objetivoAprendizajeTransversal);
			}
			PostDelete(context);
		}
	}
}