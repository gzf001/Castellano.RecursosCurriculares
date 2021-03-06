using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
    public partial class ObjetivoAprendizaje
    {
        const string urlSincronizacion = "/api/BasesCurriculares/ObjetivosAprendizaje";

        public static bool Exists(RecursoCurricular.Educacion.Grado grado, RecursoCurricular.Educacion.Sector sector, RecursoCurricular.BaseCurricular.Eje eje)
        {
            return Query.GetObjetivoAprendizajes(grado, sector, eje).Any<RecursoCurricular.BaseCurricular.ObjetivoAprendizaje>();
        }

        public static int Last(RecursoCurricular.Educacion.Grado grado, RecursoCurricular.Educacion.Sector sector, RecursoCurricular.BaseCurricular.Eje eje)
        {
            IQueryable<ObjetivoAprendizaje> objetivosAprendizaje = Query.GetObjetivoAprendizajes(grado, sector, eje);

            return objetivosAprendizaje.Count<ObjetivoAprendizaje>() > 0 ? objetivosAprendizaje.Count<ObjetivoAprendizaje>() + 1 : 1;
        }

        public static ObjetivoAprendizaje Get(int tipoEducacionCodigo, int anioNumero, int gradoCodigo, Guid sectorId, Guid ejeId, Guid id)
        {
            return Query.GetObjetivoAprendizajes().SingleOrDefault<RecursoCurricular.BaseCurricular.ObjetivoAprendizaje>(x => x.TipoEducacionCodigo.Equals(tipoEducacionCodigo) && x.AnoNumero.Equals(anioNumero) && x.GradoCodigo.Equals(gradoCodigo) && x.SectorId.Equals(sectorId) && x.EjeId.Equals(ejeId) && x.Id.Equals(id));
        }

        public static List<ObjetivoAprendizaje> GetAll()
        {
            return
                (
                from query in Query.GetObjetivoAprendizajes()
                select query
                ).ToList<ObjetivoAprendizaje>();
        }

        public static List<ObjetivoAprendizaje> GetAll(RecursoCurricular.Educacion.Grado grado, RecursoCurricular.Educacion.Sector sector, RecursoCurricular.BaseCurricular.Eje eje)
        {
            return
                (
                from query in Query.GetObjetivoAprendizajes(grado, sector, eje)
                orderby query.Numero
                select query
                ).ToList<ObjetivoAprendizaje>();
        }

        /// <summary>
        /// Retorna todos los objetivos de aprendizaje que tengan indicadores asociados
        /// </summary>
        /// <param name="grado"></param>
        /// <param name="sector"></param>
        /// <param name="eje"></param>
        /// <returns></returns>
        public static List<ObjetivoAprendizaje> GetObjetivosAprendizajeIndicador(RecursoCurricular.Anio anio, RecursoCurricular.Educacion.Grado grado, RecursoCurricular.Educacion.Sector sector, RecursoCurricular.BaseCurricular.Eje eje)
        {
            return
                (from objetivoAprendizaje in Query.GetObjetivoAprendizajes()
                 where Query.GetIndicadores(anio, sector, grado, eje).Select<RecursoCurricular.BaseCurricular.Indicador, RecursoCurricular.BaseCurricular.ObjetivoAprendizaje>(x => x.ObjetivoAprendizaje).Contains<RecursoCurricular.BaseCurricular.ObjetivoAprendizaje>(objetivoAprendizaje)
                 orderby objetivoAprendizaje.Numero
                 select objetivoAprendizaje).ToList<RecursoCurricular.BaseCurricular.ObjetivoAprendizaje>();
        }

        public void SyncUp()
        {
            RecursoCurricular.BaseCurricular.PassingObject.ObjetivoAprendizaje objetivoAprendizaje = new RecursoCurricular.BaseCurricular.PassingObject.ObjetivoAprendizaje
            {
                TipoEducacionCodigo = this.TipoEducacionCodigo,
                AnoNumero = this.AnoNumero,
                GradoCodigo = this.GradoCodigo,
                SectorId = this.SectorId,
                EjeId = this.EjeId,
                Id = this.Id,
                Numero = this.Numero,
                AmbitoCodigo = 1,
                SostenedorId = default(Guid),
                EstablecimientoId = default(Guid),
                Descripcion = this.Descripcion,
                UrlSincronizacion = urlSincronizacion
            };

            RecursoCurricular.Synchronization synchronization = new RecursoCurricular.Synchronization();

            synchronization.SyncUp(objetivoAprendizaje);
        }
    }
}