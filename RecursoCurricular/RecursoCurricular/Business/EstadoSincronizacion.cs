using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular
{
	public partial class EstadoSincronizacion
	{
        public static EstadoSincronizacion Sincronizado
        {
            get
            {
                return RecursoCurricular.EstadoSincronizacion.Get(1);
            }
        }

        public static EstadoSincronizacion SincronizadoConAdvertencias
        {
            get
            {
                return RecursoCurricular.EstadoSincronizacion.Get(2);
            }
        }

        public static EstadoSincronizacion NoSincronizado
        {
            get
            {
                return RecursoCurricular.EstadoSincronizacion.Get(3);
            }
        }

        public static EstadoSincronizacion Get(int codigo)
        {
            return Query.GetEstadoSincronizaciones().SingleOrDefault<RecursoCurricular.EstadoSincronizacion>(x => x.Codigo.Equals(codigo));
        }

		public static List<EstadoSincronizacion> GetAll()
		{
			return
				(
				from query in Query.GetEstadoSincronizaciones()
				select query
				).ToList<EstadoSincronizacion>();
		}
	}
}