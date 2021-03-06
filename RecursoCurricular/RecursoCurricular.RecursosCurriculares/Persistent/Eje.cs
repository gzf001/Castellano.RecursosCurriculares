using System;
using System.Linq;
namespace RecursoCurricular.RecursosCurriculares
{
	public partial class Eje
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			Eje eje = context.Ejes.SingleOrDefault<Eje>(x => x == this);

			if (eje == null)
			{
				eje = new Eje
				{
					AnoNumero = this.AnoNumero,
					SectorId = this.SectorId,
					Id = this.Id
				};

				context.Ejes.InsertOnSubmit(eje);
			}

			eje.Numero = this.Numero;
			eje.Nombre = this.Nombre;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			Eje eje = context.Ejes.SingleOrDefault<Eje>(x => x == this);

			if (eje != null)
			{
				context.Ejes.DeleteOnSubmit(eje);
			}
			PostDelete(context);
		}
	}
}