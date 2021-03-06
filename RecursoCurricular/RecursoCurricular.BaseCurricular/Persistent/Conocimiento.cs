using System;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
	public partial class Conocimiento
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			Conocimiento conocimiento = context.Conocimientos.SingleOrDefault<Conocimiento>(x => x == this);

			if (conocimiento == null)
			{
				conocimiento = new Conocimiento
				{
					TipoEducacionCodigo = this.TipoEducacionCodigo,
					AnoNumero = this.AnoNumero,
					SectorId = this.SectorId,
					Id = this.Id
				};

				context.Conocimientos.InsertOnSubmit(conocimiento);
			}

			conocimiento.Numero = this.Numero;
			conocimiento.Nombre = this.Nombre;
			conocimiento.Descripcion = this.Descripcion;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			Conocimiento conocimiento = context.Conocimientos.SingleOrDefault<Conocimiento>(x => x == this);

			if (conocimiento != null)
			{
				context.Conocimientos.DeleteOnSubmit(conocimiento);
			}
			PostDelete(context);
		}
	}
}