using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.RecursosCurriculares
{
    public partial class ObjetivoVertical
    {
        const string urlSincronizacion = "/api/RecursosCurriculares/ObjetivosVerticales";

        public static int Count(RecursoCurricular.RecursosCurriculares.Aprendizaje aprendizaje)
        {
            return Query.GetObjetivoVerticales(aprendizaje).Count<RecursoCurricular.RecursosCurriculares.ObjetivoVertical>();
        }

        public static int Last(RecursoCurricular.Anio anio, RecursoCurricular.Educacion.Grado grado, RecursoCurricular.Educacion.Sector sector)
        {
            IQueryable<ObjetivoVertical> objetivosVerticales = Query.GetObjetivoVerticales(anio, grado, sector);

            return objetivosVerticales.Count<ObjetivoVertical>() > 0 ? objetivosVerticales.Count<ObjetivoVertical>() + 1 : 1;
        }

        public static ObjetivoVertical Get(int anioNumero, int tipoEducacionCodigo, int gradoCodigo, Guid sectorId, Guid id)
        {
            return Query.GetObjetivoVerticales().SingleOrDefault<ObjetivoVertical>(x => x.AnoNumero.Equals(anioNumero) && x.TipoEducacionCodigo.Equals(tipoEducacionCodigo) && x.GradoCodigo.Equals(gradoCodigo) && x.SectorId.Equals(sectorId) && x.Id.Equals(id));
        }

        public static List<ObjetivoVertical> GetAll()
        {
            return
                (
                from query in Query.GetObjetivoVerticales()
                select query
                ).ToList<ObjetivoVertical>();
        }

        public static List<ObjetivoVertical> GetAll(RecursoCurricular.Anio anio, RecursoCurricular.Educacion.Grado grado, RecursoCurricular.Educacion.Sector sector)
        {
            return
                (
                from query in Query.GetObjetivoVerticales(anio, grado, sector)
                orderby query.Numero
                select query
                ).ToList<ObjetivoVertical>();
        }

        public static List<ObjetivoVertical> GetAll(RecursoCurricular.RecursosCurriculares.Aprendizaje aprendizaje)
        {
            return
                (
                from query in Query.GetObjetivoVerticales(aprendizaje)
                orderby query.Numero
                select query
                ).ToList<ObjetivoVertical>();
        }

        public void SyncUp()
        {
            RecursoCurricular.RecursosCurriculares.PassingObject.ObjetivoVerticalAno objetivoVerticalAno = new RecursoCurricular.RecursosCurriculares.PassingObject.ObjetivoVerticalAno
            {
                TipoEducacionCodigo = this.TipoEducacionCodigo,
                GradoCodigo = this.GradoCodigo,
                SectorId = this.SectorId,
                ObjetivoVerticalId = this.Id,
                AnoNumero = this.AnoNumero,
                ObjetivoVertical = new RecursoCurricular.RecursosCurriculares.PassingObject.ObjetivoVertical
                {
                    TipoEducacionCodigo = this.TipoEducacionCodigo,
                    GradoCodigo = this.GradoCodigo,
                    SectorId = this.SectorId,
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

            synchronization.SyncUp(objetivoVerticalAno);
        }
    }
}