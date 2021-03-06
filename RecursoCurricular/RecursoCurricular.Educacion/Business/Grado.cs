using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RecursoCurricular.Educacion
{
    public partial class Grado : RecursoCurricular.Default
    {
        public static IEnumerable<SelectListItem> Grados(int tipoEducacionCodigo)
        {
            RecursoCurricular.Educacion.TipoEducacion tipoEducacion = RecursoCurricular.Educacion.TipoEducacion.Get(tipoEducacionCodigo);

            List<RecursoCurricular.Educacion.Grado> grados = RecursoCurricular.Educacion.Grado.GetAll(tipoEducacion);

            SelectList lista = new SelectList(grados, "Codigo", "Nombre");

            return Comuna.DefaultItem.Concat(lista);
        }

        public static Grado Get(int tipoEducacionCodigo, int gradoCodigo)
        {
            return Query.GetGrados().SingleOrDefault<RecursoCurricular.Educacion.Grado>(x => x.TipoEducacionCodigo.Equals(tipoEducacionCodigo) && x.Codigo.Equals(gradoCodigo));
        }

        public static List<Grado> GetAll()
        {
            return
                (
                from query in Query.GetGrados()
                select query
                ).ToList<Grado>();
        }

        public static List<Grado> GetAll(RecursoCurricular.Educacion.TipoEducacion tipoEducacion)
        {
            return
                (
                from query in Query.GetGrados(tipoEducacion)
                orderby query.Codigo
                select query
                ).ToList<Grado>();
        }
    }
}