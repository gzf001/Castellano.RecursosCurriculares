using System;
using System.Linq;
namespace RecursoCurricular
{
	public partial class DimensionHabilidadTic
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			DimensionHabilidadTic dimensionHabilidadTic = context.DimensionHabilidadTices.SingleOrDefault<DimensionHabilidadTic>(x => x == this);

			if (dimensionHabilidadTic == null)
			{
				dimensionHabilidadTic = new DimensionHabilidadTic
				{
					Id = this.Id,
					AnoNumero = this.AnoNumero
				};

				context.DimensionHabilidadTices.InsertOnSubmit(dimensionHabilidadTic);
			}

			dimensionHabilidadTic.Numero = this.Numero;
			dimensionHabilidadTic.Nombre = this.Nombre;
			dimensionHabilidadTic.Descripcion = this.Descripcion;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			DimensionHabilidadTic dimensionHabilidadTic = context.DimensionHabilidadTices.SingleOrDefault<DimensionHabilidadTic>(x => x == this);

			if (dimensionHabilidadTic != null)
			{
				context.DimensionHabilidadTices.DeleteOnSubmit(dimensionHabilidadTic);
			}
			PostDelete(context);
		}
	}
}