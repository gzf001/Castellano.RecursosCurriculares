using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
    public partial class Indicador
    {
        const string urlSincronizacion = "/api/BasesCurriculares/Indicadores";

        public static int Last(RecursoCurricular.BaseCurricular.ObjetivoAprendizaje objetivoAprendizaje)
        {
            IQueryable<Indicador> indicadores = Query.GetIndicadores(objetivoAprendizaje);

            return indicadores.Count<Indicador>() > 0 ? indicadores.Count<Indicador>() + 1 : 1;
        }

        public static Indicador Get(int tipoEducacionCodigo, int anioNumero, int gradoCodigo, Guid sectorId, Guid ejeId, Guid objetivoAprendizajeId, Guid id)
        {
            return Query.GetIndicadores().SingleOrDefault<Indicador>(x => x.TipoEducacionCodigo.Equals(tipoEducacionCodigo) && x.AnoNumero.Equals(anioNumero) && x.GradoCodigo.Equals(gradoCodigo) && x.SectorId.Equals(sectorId) && x.EjeId.Equals(ejeId) && x.ObjetivoAprendizajeId.Equals(objetivoAprendizajeId) && x.Id.Equals(id));
        }

        public static List<Indicador> GetAll()
        {
            return
                (
                from query in Query.GetIndicadores()
                select query
                ).ToList<Indicador>();
        }

        public static List<Indicador> GetAll(RecursoCurricular.BaseCurricular.ObjetivoAprendizaje objetivoAprendizaje)
        {
            return
                (
                from query in Query.GetIndicadores(objetivoAprendizaje)
                orderby query.Numero
                select query
                ).ToList<Indicador>();
        }

        public void SyncUp()
        {
            RecursoCurricular.BaseCurricular.PassingObject.Indicador indicador = new RecursoCurricular.BaseCurricular.PassingObject.Indicador
            {
                TipoEducacionCodigo = this.TipoEducacionCodigo,
                AnoNumero = this.AnoNumero,
                GradoCodigo = this.GradoCodigo,
                SectorId = this.SectorId,
                EjeId = this.EjeId,
                ObjetivoAprendizajeId = this.ObjetivoAprendizajeId,
                Id = this.Id,
                Numero = this.Numero,
                AmbitoCodigo = 1,
                SostenedorId = default(Guid),
                EstablecimientoId = default(Guid),
                Descripcion = this.Descripcion,
                UrlSincronizacion = urlSincronizacion
            };

            RecursoCurricular.Synchronization synchronization = new RecursoCurricular.Synchronization();

            synchronization.SyncUp(indicador);
        }
    }
}