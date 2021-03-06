using System;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
	public partial class UnidadSubHabilidad
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			UnidadSubHabilidad unidadSubHabilidad = context.UnidadSubHabilidades.SingleOrDefault<UnidadSubHabilidad>(x => x == this);

			if (unidadSubHabilidad == null)
			{
				unidadSubHabilidad = new UnidadSubHabilidad
				{
					TipoEducacionCodigo = this.TipoEducacionCodigo,
					AnoNumero = this.AnoNumero,
					GradoCodigo = this.GradoCodigo,
					SectorId = this.SectorId,
					UnidadId = this.UnidadId,
					HabilidadId = this.HabilidadId,
					SubHabilidadId = this.SubHabilidadId
				};

				context.UnidadSubHabilidades.InsertOnSubmit(unidadSubHabilidad);
			}

			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			UnidadSubHabilidad unidadSubHabilidad = context.UnidadSubHabilidades.SingleOrDefault<UnidadSubHabilidad>(x => x == this);

			if (unidadSubHabilidad != null)
			{
				context.UnidadSubHabilidades.DeleteOnSubmit(unidadSubHabilidad);
			}
			PostDelete(context);
		}
	}
}