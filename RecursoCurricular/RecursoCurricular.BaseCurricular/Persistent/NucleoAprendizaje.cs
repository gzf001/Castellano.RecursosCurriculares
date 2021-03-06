using System;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
	public partial class NucleoAprendizaje
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			NucleoAprendizaje nucleoAprendizaje = context.NucleoAprendizajes.SingleOrDefault<NucleoAprendizaje>(x => x == this);

			if (nucleoAprendizaje == null)
			{
				nucleoAprendizaje = new NucleoAprendizaje
				{
					AnoNumero = this.AnoNumero,
					AmbitoExperienciaAprendizajeCodigo = this.AmbitoExperienciaAprendizajeCodigo,
					Id = this.Id
				};

				context.NucleoAprendizajes.InsertOnSubmit(nucleoAprendizaje);
			}

			nucleoAprendizaje.Numero = this.Numero;
			nucleoAprendizaje.Nombre = this.Nombre;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			NucleoAprendizaje nucleoAprendizaje = context.NucleoAprendizajes.SingleOrDefault<NucleoAprendizaje>(x => x == this);

			if (nucleoAprendizaje != null)
			{
				context.NucleoAprendizajes.DeleteOnSubmit(nucleoAprendizaje);
			}
			PostDelete(context);
		}
	}
}