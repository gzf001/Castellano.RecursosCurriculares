using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RecursoCurricular.BaseCurricular
{
    public partial class AmbitoExperienciaAprendizaje : RecursoCurricular.Default
    {
        const string urlSincronizacion = "/api/BasesCurriculares/AmbitoExperienciasAprendizaje";

        public static IEnumerable<SelectListItem> AmbitosExperienciaAprendizaje(RecursoCurricular.Anio anio)
        {
            SelectList lista = new SelectList(RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje.GetAll(anio), "Codigo", "Nombre");

            return DefaultItem.Concat(lista);
        }

        public static AmbitoExperienciaAprendizaje Get(int anioNumero, int ambitoExperienciaAprendizajeCodigo)
        {
            return Query.GetAmbitoExperienciaAprendizajes().SingleOrDefault<RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje>(x => x.AnoNumero.Equals(anioNumero) && x.Codigo.Equals(ambitoExperienciaAprendizajeCodigo));
        }

        public static List<AmbitoExperienciaAprendizaje> GetAll()
        {
            return
                (
                from query in Query.GetAmbitoExperienciaAprendizajes()
                select query
                ).ToList<AmbitoExperienciaAprendizaje>();
        }

        public static List<AmbitoExperienciaAprendizaje> GetAll(RecursoCurricular.Anio anio)
        {
            return
                (
                from query in Query.GetAmbitoExperienciaAprendizajes(anio)
                orderby query.Codigo
                select query
                ).ToList<AmbitoExperienciaAprendizaje>();
        }
    }
}