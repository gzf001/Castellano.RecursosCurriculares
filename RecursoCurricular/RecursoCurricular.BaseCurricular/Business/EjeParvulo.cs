using System;
using System.Collections.Generic;
using System.Linq;
using RecursoCurricular.Educacion;
using System.Web.Mvc;

namespace RecursoCurricular.BaseCurricular
{
    public partial class EjeParvulo : RecursoCurricular.Default
    {
        const string urlSincronizacion = "/api/BasesCurriculares/EjesParvulo";

        public static IEnumerable<SelectListItem> Ejes(AmbitoExperienciaAprendizaje ambitoExperienciaAprendizaje, NucleoAprendizaje nucleo, Ciclo ciclo)
        {
            List<RecursoCurricular.BaseCurricular.EjeParvulo> ejes = RecursoCurricular.BaseCurricular.EjeParvulo.GetAll(ambitoExperienciaAprendizaje, nucleo, ciclo);

            SelectList lista = new SelectList(ejes, "Id", "Nombre");

            return EjeParvulo.DefaultItem.Concat(lista);
        }

        public static int Last(AmbitoExperienciaAprendizaje ambitoExperienciaAprendizaje, NucleoAprendizaje nucleAprendizaje, Ciclo ciclo)
        {
            IQueryable<EjeParvulo> ejes = Query.GetEjeParvulos(ambitoExperienciaAprendizaje, nucleAprendizaje, ciclo);

            return ejes.Count<EjeParvulo>() > 0 ? ejes.Count<EjeParvulo>() + 1 : 1;
        }

        public static EjeParvulo Get(int anioNumero, int ambitoExperienciaAprendizajeCodigo, Guid nucleAprendizajeId, int cicloCodigo, Guid id)
        {
            return Query.GetEjeParvulos().SingleOrDefault<RecursoCurricular.BaseCurricular.EjeParvulo>(x => x.AnoNumero.Equals(anioNumero) && x.AmbitoExperienciaAprendizajeCodigo.Equals(ambitoExperienciaAprendizajeCodigo) && x.NucleoId.Equals(nucleAprendizajeId) && x.CicloCodigo.Equals(cicloCodigo) && x.Id.Equals(id));
        }

        public static List<EjeParvulo> GetAll()
        {
            return
                (
                from query in Query.GetEjeParvulos()
                select query
                ).ToList<EjeParvulo>();
        }

        public static List<EjeParvulo> GetAll(AmbitoExperienciaAprendizaje ambitoExperienciaAprendizaje, NucleoAprendizaje nucleAprendizaje, Ciclo ciclo)
        {
            return
                (
                from query in Query.GetEjeParvulos(ambitoExperienciaAprendizaje, nucleAprendizaje, ciclo)
                orderby query.Numero
                select query
                ).ToList<EjeParvulo>();
        }
    }
}