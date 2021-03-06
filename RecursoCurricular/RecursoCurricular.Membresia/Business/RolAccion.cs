using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.Membresia
{
    public partial class RolAccion
    {
        public static bool Exists(Rol rol, MenuItem menuItem, Accion accion)
        {
            return Query.GetRolAcciones(rol, menuItem).Any<RecursoCurricular.Membresia.RolAccion>(x => x.Accion.Equals(accion));
        }

        public static bool Exists(RecursoCurricular.Persona persona, MenuItem menuItem, Accion accion)
        {
            return Query.GetRolAcciones(persona, menuItem).Any<RolAccion>(x => x.MenuItemAccion.Accion.Equals(accion));
        }

        public static RolAccion Get(Guid rolId, Guid aplicacionId, Guid menuId, Guid menuItemId, Int32 accionCodigo)
        {
            return Query.GetRolAcciones().SingleOrDefault<RolAccion>(x => x.RolId == rolId && x.AplicacionId == aplicacionId && x.MenuItemId == menuItemId && x.AccionCodigo == accionCodigo);
        }

        public static List<RolAccion> GetAll()
        {
            return
                (
                from query in Query.GetRolAcciones()
                select query
                ).ToList<RolAccion>();
        }

        public static List<RolAccion> GetAll(Aplicacion aplicacion, Rol rol)
        {
            return
                (
                from query in Query.GetRolAcciones(aplicacion, rol)
                select query
                ).ToList<RolAccion>();
        }

        public static List<RolAccion> GetAll(Persona persona)
        {
            return
                (
                from query in Query.GetRolAcciones(persona)
                orderby query.Rol.Nombre
                select query
                ).ToList<RolAccion>();
        }

        public static List<RolAccion> GetAll(Rol rol)
        {
            return Query.GetRolAcciones(rol).ToList<RolAccion>();
        }
    }
}