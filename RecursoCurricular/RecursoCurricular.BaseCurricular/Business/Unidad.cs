using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
    public partial class Unidad
    {
        public static int Last(RecursoCurricular.Anio anio, RecursoCurricular.Educacion.Grado grado, RecursoCurricular.Educacion.Sector sector)
        {
            IQueryable<Unidad> unidades = Query.GetUnidades(anio, grado, sector);

            return unidades.Count<Unidad>() > 0 ? unidades.Count<Unidad>() + 1 : 1;
        }

        public static List<Unidad> GetAll()
        {
            return
                (
                from query in Query.GetUnidades()
                select query
                ).ToList<Unidad>();
        }

        public static List<Unidad> GetAll(RecursoCurricular.Anio anio, RecursoCurricular.Educacion.Grado grado, RecursoCurricular.Educacion.Sector sector)
        {
            return
                (
                from query in Query.GetUnidades(anio, grado, sector)
                orderby query.Numero
                select query
                ).ToList<Unidad>();
        }
    }
}