using System;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
	public partial class SubHabilidad
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			SubHabilidad subHabilidad = context.SubHabilidades.SingleOrDefault<SubHabilidad>(x => x == this);

			if (subHabilidad == null)
			{
				subHabilidad = new SubHabilidad
				{
					TipoEducacionCodigo = this.TipoEducacionCodigo,
					AnoNumero = this.AnoNumero,
					GradoCodigo = this.GradoCodigo,
					HabilidadId = this.HabilidadId,
					SectorId = this.SectorId,
					Id = this.Id
				};

				context.SubHabilidades.InsertOnSubmit(subHabilidad);
			}

			subHabilidad.Numero = this.Numero;
			subHabilidad.Descripcion = this.Descripcion;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			SubHabilidad subHabilidad = context.SubHabilidades.SingleOrDefault<SubHabilidad>(x => x == this);

			if (subHabilidad != null)
			{
				context.SubHabilidades.DeleteOnSubmit(subHabilidad);
			}
			PostDelete(context);
		}
	}
}