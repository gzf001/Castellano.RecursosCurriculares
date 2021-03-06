using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
    public partial class ObjetivoAprendizajeTransversal
    {
        const string urlSincronizacion = "/api/BasesCurriculares/ObjetivosTransversales";

        public static ObjetivoAprendizajeTransversal Get(Guid id, Guid dimensionOATId, int anioNumero)
        {
            return Query.GetObjetivoAprendizajeTransversales().SingleOrDefault<RecursoCurricular.BaseCurricular.ObjetivoAprendizajeTransversal>(x => x.Id == id && x.DimensionOATId == dimensionOATId && x.AnoNumero == anioNumero);
        }

        public static int Last(RecursoCurricular.Anio anio)
        {
            IQueryable<RecursoCurricular.BaseCurricular.ObjetivoAprendizajeTransversal> objetivosAprendizajeTransversales = Query.GetObjetivoAprendizajeTransversales(anio);

            return objetivosAprendizajeTransversales.Count<RecursoCurricular.BaseCurricular.ObjetivoAprendizajeTransversal>() > 0 ? objetivosAprendizajeTransversales.Count<RecursoCurricular.BaseCurricular.ObjetivoAprendizajeTransversal>() + 1 : 1;
        }

        public static List<ObjetivoAprendizajeTransversal> GetAll()
        {
            return
                (
                from query in Query.GetObjetivoAprendizajeTransversales()
                select query
                ).ToList<ObjetivoAprendizajeTransversal>();
        }

        public static List<ObjetivoAprendizajeTransversal> GetAll(DimensionOAT dimensionOAT)
        {
            return
                (
                from query in Query.GetObjetivoAprendizajeTransversales(dimensionOAT)
                orderby query.Numero
                select query
                ).ToList<ObjetivoAprendizajeTransversal>();
        }

        public void SyncUp()
        {
            RecursoCurricular.BaseCurricular.PassingObject.ObjetivoAprendizajeTransversal objetivoAprendizajeTransversal = new RecursoCurricular.BaseCurricular.PassingObject.ObjetivoAprendizajeTransversal
            {
                DimensionOATId = this.DimensionOATId,
                AnoNumero = this.AnoNumero,
                Id = this.Id,
                AmbitoCodigo = 1,
                SostenedorId = default(Guid),
                EstablecimientoId = default(Guid),
                Numero = this.Numero,
                Nombre = this.Nombre,
                Descripcion = this.Descripcion,
                UrlSincronizacion = urlSincronizacion
            };

            RecursoCurricular.Synchronization synchronization = new RecursoCurricular.Synchronization();

            synchronization.SyncUp(objetivoAprendizajeTransversal);
        }
    }
}