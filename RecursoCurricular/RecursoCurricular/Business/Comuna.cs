using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RecursoCurricular
{
	public partial class Comuna : RecursoCurricular.Default
    {
        public static IEnumerable<SelectListItem> Comunas(short regionCodigo, short ciudadCodigo)
        {
            RecursoCurricular.Ciudad ciudad = RecursoCurricular.Ciudad.Get(regionCodigo, ciudadCodigo);

            List<RecursoCurricular.Comuna> comunas = RecursoCurricular.Comuna.GetAll(ciudad);

            SelectList lista = new SelectList(comunas, "Codigo", "Nombre");

            return Comuna.DefaultItem.Concat(lista);
        }

        public static Comuna Get(int regionCodigo, int ciudadCodigo, int codigo)
        {
            return Query.GetComunas().SingleOrDefault<Comuna>(x => x.RegionCodigo == regionCodigo && x.CiudadCodigo == ciudadCodigo && x.Codigo == codigo);
        }

        public static Comuna Get(string nombre)
        {
            return Query.GetComunas().SingleOrDefault<Comuna>(x => x.Nombre == nombre);
        }

        public static List<Comuna> GetAll()
        {
            return
                (
                from query in Query.GetComunas()
                orderby query.Nombre
                select query
                ).ToList<Comuna>();
        }

        public static List<Comuna> GetAll(Ciudad ciudad)
        {
            return
                (
                from query in Query.GetComunas(ciudad)
                orderby query.Nombre
                select query
                ).ToList<Comuna>();
        }
    }
}