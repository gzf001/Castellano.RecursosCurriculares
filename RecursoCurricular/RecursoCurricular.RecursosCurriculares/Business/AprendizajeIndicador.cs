using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.RecursosCurriculares
{
    public partial class AprendizajeIndicador
    {
        public static int Last(RecursoCurricular.RecursosCurriculares.Aprendizaje aprendizaje)
        {
            IQueryable<AprendizajeIndicador> aprendizajeIndicadores = Query.GetAprendizajeIndicadores(aprendizaje);

            return aprendizajeIndicadores.Count<AprendizajeIndicador>() > 0 ? aprendizajeIndicadores.Count<AprendizajeIndicador>() + 1 : 1;
        }

        public static AprendizajeIndicador Get(int anioNumero, int tipoEducacionCodigo, int gradoCodigo, Guid sectorId, Guid aprendizajeId, Guid id)
        {
            return Query.GetAprendizajeIndicadores().SingleOrDefault<RecursoCurricular.RecursosCurriculares.AprendizajeIndicador>(x => x.AnoNumero.Equals(anioNumero) && x.TipoEducacionCodigo.Equals(tipoEducacionCodigo) && x.GradoCodigo.Equals(gradoCodigo) && x.SectorId.Equals(sectorId) && x.AprendizajeId.Equals(aprendizajeId) && x.Id.Equals(id));
        }

        public static List<AprendizajeIndicador> GetAll()
        {
            return
                (
                from query in Query.GetAprendizajeIndicadores()
                select query
                ).ToList<AprendizajeIndicador>();
        }

        public static List<AprendizajeIndicador> GetAll(RecursoCurricular.RecursosCurriculares.Aprendizaje aprendizaje)
        {
            return
                (
                from query in Query.GetAprendizajeIndicadores(aprendizaje)
                orderby query.Numero
                select query
                ).ToList<AprendizajeIndicador>();
        }
    }
}