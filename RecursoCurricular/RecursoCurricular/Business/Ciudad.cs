using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RecursoCurricular
{
	public partial class Ciudad : RecursoCurricular.Default
	{
        public static IEnumerable<SelectListItem> Ciudades(short regionCodigo)
        {
            RecursoCurricular.Region region = RecursoCurricular.Region.Get(regionCodigo);

            List<RecursoCurricular.Ciudad> ciudades = RecursoCurricular.Ciudad.GetAll(region);

            SelectList lista = new SelectList(ciudades, "Codigo", "Nombre");

            return Ciudad.DefaultItem.Concat(lista);
        }

        public static Ciudad Get(int regionCodigo, int codigo)
        {
            return Query.GetCiudades().SingleOrDefault<Ciudad>(x => x.RegionCodigo == regionCodigo && x.Codigo == codigo);
        }

        public static List<Ciudad> GetAll()
        {
            return
                (
                from query in Query.GetCiudades()
                select query
                ).ToList<Ciudad>();
        }

        public static List<Ciudad> GetAll(Region region)
        {
            return
                (
                from query in Query.GetCiudades(region)
                select query
                ).ToList<Ciudad>();
        }
    }
}