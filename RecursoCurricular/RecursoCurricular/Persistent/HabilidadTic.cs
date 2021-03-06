using System;
using System.Linq;
namespace RecursoCurricular
{
	public partial class HabilidadTic
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			HabilidadTic habilidadTic = context.HabilidadTices.SingleOrDefault<HabilidadTic>(x => x == this);

			if (habilidadTic == null)
			{
				habilidadTic = new HabilidadTic
				{
					Id = this.Id,
					DimensionHabilidadTicId = this.DimensionHabilidadTicId,
					AnoNumero = this.AnoNumero
				};

				context.HabilidadTices.InsertOnSubmit(habilidadTic);
			}

			habilidadTic.Numero = this.Numero;
			habilidadTic.Nombre = this.Nombre;
			habilidadTic.Descripcion = this.Descripcion;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			HabilidadTic habilidadTic = context.HabilidadTices.SingleOrDefault<HabilidadTic>(x => x == this);

			if (habilidadTic != null)
			{
				context.HabilidadTices.DeleteOnSubmit(habilidadTic);
			}
			PostDelete(context);
		}
	}
}