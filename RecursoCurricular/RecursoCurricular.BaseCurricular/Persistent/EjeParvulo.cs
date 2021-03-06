using System;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
	public partial class EjeParvulo
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			EjeParvulo ejeParvulo = context.EjeParvulos.SingleOrDefault<EjeParvulo>(x => x == this);

			if (ejeParvulo == null)
			{
				ejeParvulo = new EjeParvulo
				{
					AnoNumero = this.AnoNumero,
					AmbitoExperienciaAprendizajeCodigo = this.AmbitoExperienciaAprendizajeCodigo,
					NucleoId = this.NucleoId,
					CicloCodigo = this.CicloCodigo,
					Id = this.Id
				};

				context.EjeParvulos.InsertOnSubmit(ejeParvulo);
			}

			ejeParvulo.Numero = this.Numero;
			ejeParvulo.Nombre = this.Nombre;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			EjeParvulo ejeParvulo = context.EjeParvulos.SingleOrDefault<EjeParvulo>(x => x == this);

			if (ejeParvulo != null)
			{
				context.EjeParvulos.DeleteOnSubmit(ejeParvulo);
			}
			PostDelete(context);
		}
	}
}