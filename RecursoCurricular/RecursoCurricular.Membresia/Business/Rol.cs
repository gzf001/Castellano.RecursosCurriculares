using System;
using System.Collections.Generic;
using System.Linq;

namespace RecursoCurricular.Membresia
{
    public partial class Rol
    {
        public static Rol Get(Guid id)
        {
            return Query.GetRoles().SingleOrDefault<Rol>(x => x.Id == id);
        }

        public static Rol Get(string clave)
        {
            return Query.GetRoles().SingleOrDefault<Rol>(x => x.Clave == clave);
        }

        public static List<Rol> GetAll()
        {
            return
                (
                from query in Query.GetRoles()
                orderby query.Nombre
                select query
                ).ToList<Rol>();
        }

        public static List<Rol> GetAll(Aplicacion aplicacion)
        {
            return
                (
                from query in Query.GetRoles(aplicacion)
                orderby query.Nombre
                select query
                ).ToList<Rol>();
        }
    }
}