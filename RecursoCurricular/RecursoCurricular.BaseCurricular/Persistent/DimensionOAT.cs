using System;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
	public partial class DimensionOAT
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			DimensionOAT dimensionOAT = context.DimensionOATes.SingleOrDefault<DimensionOAT>(x => x == this);

			if (dimensionOAT == null)
			{
				dimensionOAT = new DimensionOAT
				{
					Id = this.Id,
					AnoNumero = this.AnoNumero
				};

				context.DimensionOATes.InsertOnSubmit(dimensionOAT);
			}

			dimensionOAT.Numero = this.Numero;
			dimensionOAT.Nombre = this.Nombre;
			dimensionOAT.Descripcion = this.Descripcion;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			DimensionOAT dimensionOAT = context.DimensionOATes.SingleOrDefault<DimensionOAT>(x => x == this);

			if (dimensionOAT != null)
			{
				context.DimensionOATes.DeleteOnSubmit(dimensionOAT);
			}
			PostDelete(context);
		}
	}
}