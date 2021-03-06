using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.Membresia
{
	public partial class AplicacionPerfil
	{
		public static int Count(Aplicacion aplicacion)
		{
			return Query.GetAplicacionPerfiles(aplicacion).Count<AplicacionPerfil>();
		}

		public static bool Exists(Aplicacion aplicacion, Perfil perfil)
		{
			return Query.GetAplicacionPerfiles().Any<AplicacionPerfil>(x => x.Aplicacion == aplicacion && x.Perfil == perfil);
		}

		public static AplicacionPerfil Get(Guid aplicacionId, int perfilCodigo)
		{
			return Query.GetAplicacionPerfiles().SingleOrDefault<AplicacionPerfil>(x => x.AplicacionId == aplicacionId && x.PerfilCodigo == perfilCodigo);
		}

		public static List<AplicacionPerfil> GetAll()
		{
			return
				(
				from query in Query.GetAplicacionPerfiles()
				select query
				).ToList<AplicacionPerfil>();
		}

        public static List<AplicacionPerfil> GetAll(Aplicacion aplicacion)
        {
            return
                (
                from query in Query.GetAplicacionPerfiles(aplicacion)
                select query
                ).ToList<AplicacionPerfil>();
        }
    }
}