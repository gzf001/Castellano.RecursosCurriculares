using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.RecursosCurriculares
{
    public partial class AprendizajeObjetivoVertical
    {
        public static AprendizajeObjetivoVertical Get(int anioNumero, int tipoEducacionCodigo, int gradoCodigo, Guid sectorId, Guid aprendizajeId, Guid objetivoVerticalId)
        {
            return Query.GetAprendizajeObjetivoVerticales().SingleOrDefault<RecursoCurricular.RecursosCurriculares.AprendizajeObjetivoVertical>(x => x.AnoNumero.Equals(anioNumero) && x.TipoEducacionCodigo.Equals(tipoEducacionCodigo) && x.GradoCodigo.Equals(gradoCodigo) && x.SectorId.Equals(sectorId) && x.AprendizajeId.Equals(aprendizajeId) && x.ObjetivoVerticalId.Equals(objetivoVerticalId));
        }

        public static List<AprendizajeObjetivoVertical> GetAll()
        {
            return
                (
                from query in Query.GetAprendizajeObjetivoVerticales()
                select query
                ).ToList<AprendizajeObjetivoVertical>();
        }

        public static List<AprendizajeObjetivoVertical> GetAll(RecursoCurricular.RecursosCurriculares.Aprendizaje aprendizaje)
        {
            return
                (
                from query in Query.GetAprendizajeObjetivoVerticales(aprendizaje)
                orderby query.ObjetivoVertical.Numero
                select query
                ).ToList<AprendizajeObjetivoVertical>();
        }
    }
}