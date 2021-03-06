using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RecursoCurricular.BaseCurricular
{
    public partial class NucleoAprendizaje : RecursoCurricular.Default
    {
        const string urlSincronizacion = "/api/BasesCurriculares/NucleosAprendizaje";

        public static IEnumerable<SelectListItem> Nucleos(int anioNumero, int ambitoAprendizajeCodigo)
        {
            RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje ambitoExperienciaAprendizaje = RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje.Get(anioNumero, ambitoAprendizajeCodigo);

            SelectList lista = new SelectList(RecursoCurricular.BaseCurricular.NucleoAprendizaje.GetAll(ambitoExperienciaAprendizaje), "Id", "Nombre");

            return DefaultItem.Concat(lista);
        }

        public static NucleoAprendizaje Get(int anioNumero, int ambitoExperienciaAprendizajeCodigo, Guid id)
        {
            return Query.GetNucleoAprendizajes().SingleOrDefault<RecursoCurricular.BaseCurricular.NucleoAprendizaje>(x => x.AnoNumero.Equals(anioNumero) && x.AmbitoExperienciaAprendizajeCodigo.Equals(ambitoExperienciaAprendizajeCodigo) && x.Id.Equals(id));
        }

        public static int Last(RecursoCurricular.Anio anio)
        {
            IQueryable<NucleoAprendizaje> nucleosAprendizaje = Query.GetNucleoAprendizajes(anio);

            return nucleosAprendizaje.Count<NucleoAprendizaje>() > 0 ? nucleosAprendizaje.Count<NucleoAprendizaje>() + 1 : 1;
        }

        public static List<NucleoAprendizaje> GetAll()
        {
            return
                (
                from query in Query.GetNucleoAprendizajes()
                select query
                ).ToList<NucleoAprendizaje>();
        }

        public static List<NucleoAprendizaje> GetAll(AmbitoExperienciaAprendizaje ambitoExperienciaAprendizaje)
        {
            return
                 (
                 from query in Query.GetNucleoAprendizajes(ambitoExperienciaAprendizaje)
                 orderby query.Numero
                 select query
                 ).ToList<NucleoAprendizaje>();
        }
    }
}