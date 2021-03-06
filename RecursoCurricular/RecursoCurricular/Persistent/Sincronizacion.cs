using System;
using System.Linq;
namespace RecursoCurricular
{
	public partial class Sincronizacion
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			Sincronizacion sincronizacion = context.Sincronizaciones.SingleOrDefault<Sincronizacion>(x => x == this);

			if (sincronizacion == null)
			{
				sincronizacion = new Sincronizacion
				{
					Id = this.Id
				};

				context.Sincronizaciones.InsertOnSubmit(sincronizacion);
			}

			sincronizacion.SistemaId = this.SistemaId;
			sincronizacion.EstadoSincronizacionCodigo = this.EstadoSincronizacionCodigo;
			sincronizacion.Tipo = this.Tipo;
			sincronizacion.Objeto = this.Objeto;
			sincronizacion.Detalle = this.Detalle;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			Sincronizacion sincronizacion = context.Sincronizaciones.SingleOrDefault<Sincronizacion>(x => x == this);

			if (sincronizacion != null)
			{
				context.Sincronizaciones.DeleteOnSubmit(sincronizacion);
			}
			PostDelete(context);
		}
	}
}