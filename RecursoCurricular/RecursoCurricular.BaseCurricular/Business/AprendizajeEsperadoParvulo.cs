using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
    public partial class AprendizajeEsperadoParvulo
    {
        const string urlSincronizacion = "/api/BasesCurriculares/AprendizajesEsperadosParvulo";

        public static int Last(RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje ambitoExperienciaAprendizaje, RecursoCurricular.BaseCurricular.NucleoAprendizaje nucleo, RecursoCurricular.Educacion.Ciclo ciclo, RecursoCurricular.BaseCurricular.EjeParvulo eje)
        {
            IQueryable<AprendizajeEsperadoParvulo> aprendizajeEsperadosParvulos;

            if (eje == null)
            {
                aprendizajeEsperadosParvulos = Query.GetAprendizajeEsperadoParvulos(ambitoExperienciaAprendizaje, nucleo, ciclo).Where<AprendizajeEsperadoParvulo>(x => !x.EjeParvuloId.HasValue);
            }
            else
            {
                aprendizajeEsperadosParvulos = Query.GetAprendizajeEsperadoParvulos(ambitoExperienciaAprendizaje, nucleo, ciclo, eje);
            }

            return aprendizajeEsperadosParvulos.Count<AprendizajeEsperadoParvulo>() > 0 ? aprendizajeEsperadosParvulos.Count<AprendizajeEsperadoParvulo>() + 1 : 1;
        }

        public static AprendizajeEsperadoParvulo Get(int anioNumero, int ambitoExperienciaAprendizajeCodigo, Guid nucleoAprendizajeId, int cicloCodigo, Guid id)
        {
            return Query.GetAprendizajeEsperadoParvulos().SingleOrDefault<RecursoCurricular.BaseCurricular.AprendizajeEsperadoParvulo>(x => x.AnoNumero.Equals(anioNumero) && x.AmbitoExperienciaAprendizajeCodigo.Equals(ambitoExperienciaAprendizajeCodigo) && x.NucleoAprendizajeId.Equals(nucleoAprendizajeId) && x.CicloCodigo.Equals(cicloCodigo) && x.Id.Equals(id));
        }

        public static List<AprendizajeEsperadoParvulo> GetAll()
        {
            return
                (
                from query in Query.GetAprendizajeEsperadoParvulos()
                select query
                ).ToList<AprendizajeEsperadoParvulo>();
        }

        public static List<AprendizajeEsperadoParvulo> GetAll(RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje ambitoExperienciaAprendizaje, RecursoCurricular.BaseCurricular.NucleoAprendizaje nucleoAprendizaje, RecursoCurricular.Educacion.Ciclo ciclo)
        {
            return
                (
                from query in Query.GetAprendizajeEsperadoParvulos(ambitoExperienciaAprendizaje, nucleoAprendizaje, ciclo)
                orderby query.EjeParvuloId.HasValue ? query.EjeParvulo.Numero : 0, query.Numero
                select query
                ).ToList<AprendizajeEsperadoParvulo>();
        }
    }
}