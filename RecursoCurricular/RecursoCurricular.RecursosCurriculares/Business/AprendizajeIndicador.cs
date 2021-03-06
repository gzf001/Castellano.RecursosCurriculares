using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.RecursosCurriculares
{
    public partial class AprendizajeIndicador
    {
        const string urlSincronizacion = "/api/RecursosCurriculares/AprendizajeIndicadores";

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

        public static List<AprendizajeIndicador> GetAll(RecursoCurricular.Anio anio, RecursoCurricular.Educacion.Grado grado, RecursoCurricular.Educacion.Sector sector)
        {
            return
                (
                from query in Query.GetAprendizajeIndicadores(anio, grado, sector)
                orderby query.Numero
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

        public void SyncUp()
        {
            RecursoCurricular.RecursosCurriculares.PassingObject.AprendizajeIndicadorAno aprendizajeIndicadorAno = new RecursoCurricular.RecursosCurriculares.PassingObject.AprendizajeIndicadorAno
            {
                TipoEducacionCodigo = this.TipoEducacionCodigo,
                GradoCodigo = this.GradoCodigo,
                SectorId = this.SectorId,
                AprendizajeId = this.AprendizajeId,
                AprendizajeIndicadorId = this.Id,
                AnoNumero = this.AnoNumero,
                AprendizajeIndicador = new RecursoCurricular.RecursosCurriculares.PassingObject.AprendizajeIndicador
                {
                    TipoEducacionCodigo = this.TipoEducacionCodigo,
                    GradoCodigo = this.GradoCodigo,
                    SectorId = this.SectorId,
                    AprendizajeId = this.AprendizajeId,
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

            synchronization.SyncUp(aprendizajeIndicadorAno);
        }
    }
}