using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.RecursosCurriculares
{
    public partial class ObjetivoTransversalIndicador
    {
        const string urlSincronizacion = "/api/RecursosCurriculares/ObjetivoTransversalIndicadores";

        public static ObjetivoTransversalIndicador Get(int anioNumero, int tipoEducacionCodigo, int gradoCodigo, Guid sectorId, Guid unidadId, Guid objetivoTransversalId, Guid id)
        {
            return Query.GetObjetivoTransversalIndicadores().SingleOrDefault<RecursoCurricular.RecursosCurriculares.ObjetivoTransversalIndicador>(x => x.AnoNumero.Equals(anioNumero) && x.TipoEducacionCodigo.Equals(tipoEducacionCodigo) && x.GradoCodigo.Equals(gradoCodigo) && x.SectorId.Equals(sectorId) && x.UnidadId.Equals(unidadId) && x.ObjetivoTransversalId.Equals(objetivoTransversalId) && x.Id.Equals(id));
        }

        public static int Last(RecursoCurricular.RecursosCurriculares.ObjetivoTransversal objetivoTransversal)
        {
            IQueryable<ObjetivoTransversalIndicador> objetivoTransversalIndicadores = Query.GetObjetivoTransversalIndicadores(objetivoTransversal);

            return objetivoTransversalIndicadores.Count<ObjetivoTransversalIndicador>() > 0 ? objetivoTransversalIndicadores.Count<ObjetivoTransversalIndicador>() + 1 : 1;
        }

        public static List<ObjetivoTransversalIndicador> GetAll()
        {
            return
                (
                from query in Query.GetObjetivoTransversalIndicadores()
                select query
                ).ToList<ObjetivoTransversalIndicador>();
        }

        public static List<ObjetivoTransversalIndicador> GetAll(RecursoCurricular.RecursosCurriculares.ObjetivoTransversal objetivoTransversal)
        {
            return
                (
                from query in Query.GetObjetivoTransversalIndicadores(objetivoTransversal)
                orderby query.Numero
                select query
                ).ToList<ObjetivoTransversalIndicador>();
        }

        public void SyncUp()
        {
            RecursoCurricular.RecursosCurriculares.PassingObject.ObjetivoTransversalIndicadorAno objetivoTransversalIndicadorAnio = new RecursoCurricular.RecursosCurriculares.PassingObject.ObjetivoTransversalIndicadorAno
            {
                TipoEducacionCodigo = this.TipoEducacionCodigo,
                GradoCodigo = this.GradoCodigo,
                SectorId = this.SectorId,
                UnidadId = this.UnidadId,
                ObjetivoTransversalId = this.ObjetivoTransversalId,
                ObjetivoTransversalIndicadorId = this.Id,
                AnoNumero = this.AnoNumero,
                ObjetivoTransversalIndicador = new RecursoCurricular.RecursosCurriculares.PassingObject.ObjetivoTransversalIndicador
                {
                    TipoEducacionCodigo = this.TipoEducacionCodigo,
                    GradoCodigo = this.GradoCodigo,
                    SectorId = this.SectorId,
                    UnidadId = this.UnidadId,
                    ObjetivoTransversalId = this.ObjetivoTransversalId,
                    Id = this.Id,
                    AmbitoCodigo = 1,
                    SostenedorId = default(Guid),
                    EstablecimientoId = default(Guid),
                    Numero = this.Numero,
                    Descripcion = this.Descripcion
                },
                UrlSincronizacion = urlSincronizacion
            };

            RecursoCurricular.Synchronization synchronization = new RecursoCurricular.Synchronization();

            synchronization.SyncUp(objetivoTransversalIndicadorAnio);
        }
    }
}