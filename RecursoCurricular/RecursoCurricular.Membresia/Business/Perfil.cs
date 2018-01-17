using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.Membresia
{
    public partial class Perfil
    {
        public static Perfil PerfilAnio
        {
            get
            {
                return RecursoCurricular.Membresia.Perfil.Get(1);
            }
        }

        public static bool Exists(Aplicacion aplicacion, Perfil perfil)
        {
            return Query.GetAplicacionPerfiles().Any<AplicacionPerfil>(x => x.Aplicacion == aplicacion && x.Perfil == perfil);
        }

        public static Perfil Get(int codigo)
        {
            return Query.GetPerfiles().SingleOrDefault<Perfil>(x => x.Codigo == codigo);
        }

        public static List<Perfil> GetAll()
        {
            return
                (
                from query in Query.GetPerfiles()
                orderby query.Nombre
                select query
                ).ToList<Perfil>();
        }

        public static List<Perfil> GetAll(Aplicacion aplicacion)
        {
            return
                (
                from query in Query.GetPerfiles(aplicacion)
                orderby query.Nombre
                select query
                ).ToList<Perfil>();
        }

        #region Año

        public static Anio GetAnio(Usuario usuario)
        {
            PerfilUsuario perfilUsuario = PerfilUsuario.Get(Perfil.PerfilAnio, usuario);

            if (perfilUsuario == null)
            {
                return null;
            }
            else
            {
                return RecursoCurricular.Anio.Get(int.Parse(perfilUsuario.Valor));
            }
        }

        public static void SetAnio(Usuario usuario, Anio Anio)
        {
            using (Context context = new Context())
            {
                PerfilUsuario perfilUsuario = PerfilUsuario.Get(RecursoCurricular.Membresia.Perfil.PerfilAnio, usuario);

                if (Anio == null)
                {
                    if (perfilUsuario != null) perfilUsuario.Delete(context);
                }
                else
                {
                    perfilUsuario = new PerfilUsuario
                    {
                        PerfilCodigo = RecursoCurricular.Membresia.Perfil.PerfilAnio.Codigo,
                        UsuarioId = usuario.Id,
                        Valor = Anio.Numero.ToString()
                    };

                    perfilUsuario.Save(context);
                }

                context.SubmitChanges();
            }
        }

        #endregion
    }
}