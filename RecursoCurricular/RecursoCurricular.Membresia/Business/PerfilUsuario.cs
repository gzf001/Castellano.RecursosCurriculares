using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.Membresia
{
    public partial class PerfilUsuario
    {
        public static PerfilUsuario Get(int perfilCodigo, Guid usuarioId)
		{
			return Query.GetPerfilUsuarios().SingleOrDefault<PerfilUsuario>(x => x.PerfilCodigo == perfilCodigo && x.UsuarioId == usuarioId);
		}

		public static PerfilUsuario Get(Perfil perfil, Usuario usuario)
		{
			return Query.GetPerfilUsuarios().SingleOrDefault<PerfilUsuario>(x => x.Perfil == perfil && x.Usuario == usuario);
		}

		public static List<PerfilUsuario> GetAll()
		{
			return
				(
				from query in Query.GetPerfilUsuarios()
				select query
				).ToList<PerfilUsuario>();
		}

        public static void SetPerfil(RecursoCurricular.Membresia.Usuario usuario, string url, string valor)
        {
            using (RecursoCurricular.Membresia.Context context = new RecursoCurricular.Membresia.Context())
            {
                new RecursoCurricular.Membresia.PerfilUsuario
                {
                    PerfilCodigo = RecursoCurricular.Membresia.Perfil.PerfilAnio.Codigo,
                    UsuarioId = usuario.Id,
                    Url = url,
                    Valor = valor
                }.Save(context);

                context.SubmitChanges();
            }
        }
	}
}