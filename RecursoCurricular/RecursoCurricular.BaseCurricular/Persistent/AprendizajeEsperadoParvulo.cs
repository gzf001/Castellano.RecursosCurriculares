using System;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
	public partial class AprendizajeEsperadoParvulo
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			AprendizajeEsperadoParvulo aprendizajeEsperadoParvulo = context.AprendizajeEsperadoParvulos.SingleOrDefault<AprendizajeEsperadoParvulo>(x => x == this);

			if (aprendizajeEsperadoParvulo == null)
			{
				aprendizajeEsperadoParvulo = new AprendizajeEsperadoParvulo
				{
					AnoNumero = this.AnoNumero,
					AmbitoExperienciaAprendizajeCodigo = this.AmbitoExperienciaAprendizajeCodigo,
					NucleoAprendizajeId = this.NucleoAprendizajeId,
					CicloCodigo = this.CicloCodigo,
					Id = this.Id
				};

				context.AprendizajeEsperadoParvulos.InsertOnSubmit(aprendizajeEsperadoParvulo);
			}

			aprendizajeEsperadoParvulo.EjeParvuloId = this.EjeParvuloId == default(Guid) ? null : this.EjeParvuloId;
			aprendizajeEsperadoParvulo.Numero = this.Numero;
			aprendizajeEsperadoParvulo.Descripcion = this.Descripcion;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			AprendizajeEsperadoParvulo aprendizajeEsperadoParvulo = context.AprendizajeEsperadoParvulos.SingleOrDefault<AprendizajeEsperadoParvulo>(x => x == this);

			if (aprendizajeEsperadoParvulo != null)
			{
				context.AprendizajeEsperadoParvulos.DeleteOnSubmit(aprendizajeEsperadoParvulo);
			}
			PostDelete(context);
		}
	}
}