using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
    public partial class Unidad
    {
        public static int Last(RecursoCurricular.Anio anio, RecursoCurricular.Educacion.Grado grado, RecursoCurricular.Educacion.Sector sector)
        {
            IQueryable<Unidad> unidades = Query.GetUnidades(anio, grado, sector);

            return unidades.Count<Unidad>() > 0 ? unidades.Count<Unidad>() + 1 : 1;
        }

        public static Unidad Get(int tipoEducacionCodigo, int anioNumero, int gradoCodigo, Guid SectorId, Guid id)
        {
            return Query.GetUnidades().SingleOrDefault<RecursoCurricular.BaseCurricular.Unidad>(x => x.TipoEducacionCodigo.Equals(tipoEducacionCodigo) && x.AnoNumero.Equals(anioNumero) && x.GradoCodigo.Equals(gradoCodigo) && x.SectorId.Equals(SectorId) && x.Id.Equals(id));
        }

        public static List<Unidad> GetAll()
        {
            return
                (
                from query in Query.GetUnidades()
                select query
                ).ToList<Unidad>();
        }

        public static List<Unidad> GetAll(RecursoCurricular.Anio anio, RecursoCurricular.Educacion.Grado grado, RecursoCurricular.Educacion.Sector sector)
        {
            return
                (
                from query in Query.GetUnidades(anio, grado, sector)
                orderby query.Numero
                select query
                ).ToList<Unidad>();
        }

        public static Result Save(RecursoCurricular.BaseCurricular.Unidad unidad, List<string> subHabilidades, List<string> indicadores, List<Guid> conocimientos, List<Guid> actitudes)
        {
            try
            {
                using (RecursoCurricular.BaseCurricular.Context context = new RecursoCurricular.BaseCurricular.Context())
                {
                    foreach (RecursoCurricular.BaseCurricular.UnidadSubHabilidad unidadSubhabilidad in RecursoCurricular.BaseCurricular.UnidadSubHabilidad.GetAll(unidad))
                    {
                        unidadSubhabilidad.Delete(context);
                    }

                    foreach (RecursoCurricular.BaseCurricular.UnidadObjetivoAprendizaje unidadObjetivoAprendizaje in RecursoCurricular.BaseCurricular.UnidadObjetivoAprendizaje.GetAll(unidad))
                    {
                        unidadObjetivoAprendizaje.Delete(context);
                    }

                    foreach (RecursoCurricular.BaseCurricular.UnidadIndicador unidadIndicador in RecursoCurricular.BaseCurricular.UnidadIndicador.GetAll(unidad))
                    {
                        unidadIndicador.Delete(context);
                    }

                    foreach (RecursoCurricular.BaseCurricular.UnidadActitud unidadActitud in RecursoCurricular.BaseCurricular.UnidadActitud.GetAll(unidad))
                    {
                        unidadActitud.Delete(context);
                    }

                    foreach (RecursoCurricular.BaseCurricular.UnidadConocimiento unidadConocimiento in RecursoCurricular.BaseCurricular.UnidadConocimiento.GetAll(unidad))
                    {
                        unidadConocimiento.Delete(context);
                    }

                    context.SubmitChanges();
                }

                using (RecursoCurricular.BaseCurricular.Context context = new RecursoCurricular.BaseCurricular.Context())
                {
                    int orden = 1;

                    new RecursoCurricular.BaseCurricular.Unidad
                    {
                        TipoEducacionCodigo = unidad.TipoEducacionCodigo,
                        AnoNumero = unidad.AnoNumero,
                        GradoCodigo = unidad.GradoCodigo,
                        SectorId = unidad.SectorId,
                        Id = unidad.Id,
                        Proposito = string.IsNullOrEmpty(unidad.Proposito) ? default(string) : unidad.Proposito.Trim(),
                        ConocimientoPrevio = string.IsNullOrEmpty(unidad.ConocimientoPrevio) ? default(string) : unidad.ConocimientoPrevio.Trim(),
                        PalabraClave = string.IsNullOrEmpty(unidad.PalabraClave) ? default(string) : unidad.PalabraClave.Trim(),
                        Numero = unidad.Numero,
                        Nombre = unidad.Nombre.Trim()
                    }.Save(context);

                    if (subHabilidades != null)
                    {
                        foreach (string subhabilidadId in subHabilidades)
                        {
                            Guid habilidadId = new Guid(subhabilidadId.Substring(0, 36));
                            Guid id = new Guid(subhabilidadId.Substring(36, 36));

                            RecursoCurricular.BaseCurricular.SubHabilidad subHabilidad = RecursoCurricular.BaseCurricular.SubHabilidad.Get(unidad.TipoEducacionCodigo, unidad.AnoNumero, unidad.GradoCodigo, habilidadId, unidad.SectorId, id);

                            new RecursoCurricular.BaseCurricular.UnidadSubHabilidad
                            {
                                TipoEducacionCodigo = unidad.TipoEducacionCodigo,
                                AnoNumero = unidad.AnoNumero,
                                GradoCodigo = unidad.GradoCodigo,
                                SectorId = unidad.SectorId,
                                UnidadId = unidad.Id,
                                HabilidadId = subHabilidad.HabilidadId,
                                SubHabilidadId = subHabilidad.Id
                            }.Save(context);
                        }
                    }

                    if (indicadores != null)
                    {
                        List<Guid> objetivosAprendizajeId = indicadores.Select<string, Guid>(x => new Guid(x.Substring(36, 36))).Distinct().ToList<Guid>();

                        foreach (Guid objetivoAprendizajeId in objetivosAprendizajeId)
                        {
                            RecursoCurricular.BaseCurricular.ObjetivoAprendizaje objetivoAprendizaje = Query.GetObjetivoAprendizajes().Single<RecursoCurricular.BaseCurricular.ObjetivoAprendizaje>(x => x.TipoEducacionCodigo.Equals(unidad.TipoEducacionCodigo) && x.AnoNumero.Equals(unidad.AnoNumero) && x.GradoCodigo.Equals(unidad.GradoCodigo) && x.SectorId.Equals(unidad.SectorId) && x.Id.Equals(objetivoAprendizajeId));

                            RecursoCurricular.BaseCurricular.UnidadObjetivoAprendizaje unidadObjetivoAprendizaje = RecursoCurricular.BaseCurricular.UnidadObjetivoAprendizaje.Get(unidad.TipoEducacionCodigo, unidad.AnoNumero, unidad.GradoCodigo, unidad.SectorId, unidad.Id, objetivoAprendizaje.EjeId, objetivoAprendizaje.Id);

                            new RecursoCurricular.BaseCurricular.UnidadObjetivoAprendizaje
                            {
                                TipoEducacionCodigo = unidad.TipoEducacionCodigo,
                                AnoNumero = unidad.AnoNumero,
                                GradoCodigo = unidad.GradoCodigo,
                                SectorId = unidad.SectorId,
                                UnidadId = unidad.Id,
                                EjeId = objetivoAprendizaje.EjeId,
                                ObjetivoAprendizajeId = objetivoAprendizaje.Id,
                                Orden = unidadObjetivoAprendizaje == null ? orden : unidadObjetivoAprendizaje.Orden
                            }.Save(context);
                        }

                        orden = 1;

                        foreach (string indicadorId in indicadores)
                        {
                            Guid ejeId = new Guid(indicadorId.Substring(0, 36));
                            Guid objetivoAprendizajeId = new Guid(indicadorId.Substring(36, 36));
                            Guid id = new Guid(indicadorId.Substring(72, 36));

                            RecursoCurricular.BaseCurricular.Indicador indicador = RecursoCurricular.BaseCurricular.Indicador.Get(unidad.TipoEducacionCodigo, unidad.AnoNumero, unidad.GradoCodigo, unidad.SectorId, ejeId, objetivoAprendizajeId, id);

                            RecursoCurricular.BaseCurricular.UnidadIndicador unidadIndicador = RecursoCurricular.BaseCurricular.UnidadIndicador.Get(unidad.TipoEducacionCodigo, unidad.AnoNumero, unidad.GradoCodigo, unidad.SectorId, unidad.Id, indicador.EjeId, indicador.ObjetivoAprendizajeId, indicador.Id);

                            new RecursoCurricular.BaseCurricular.UnidadIndicador
                            {
                                TipoEducacionCodigo = unidad.TipoEducacionCodigo,
                                AnoNumero = unidad.AnoNumero,
                                GradoCodigo = unidad.GradoCodigo,
                                SectorId = unidad.SectorId,
                                UnidadId = unidad.Id,
                                EjeId = indicador.EjeId,
                                ObjetivoAprendizajeId = indicador.ObjetivoAprendizajeId,
                                IndicadorId = indicador.Id,
                                Orden = unidadIndicador == null ? orden : unidadIndicador.Orden
                            }.Save(context);

                            orden++;
                        }
                    }

                    if (actitudes != null)
                    {
                        foreach (Guid actitudId in actitudes)
                        {
                            new RecursoCurricular.BaseCurricular.UnidadActitud
                            {
                                TipoEducacionCodigo = unidad.TipoEducacionCodigo,
                                AnoNumero = unidad.AnoNumero,
                                GradoCodigo = unidad.GradoCodigo,
                                SectorId = unidad.SectorId,
                                UnidadId = unidad.Id,
                                ActitudId = actitudId
                            }.Save(context);
                        }
                    }

                    if (conocimientos != null)
                    {
                        foreach (Guid conocimientoId in conocimientos)
                        {
                            new RecursoCurricular.BaseCurricular.UnidadConocimiento
                            {
                                TipoEducacionCodigo = unidad.TipoEducacionCodigo,
                                AnoNumero = unidad.AnoNumero,
                                GradoCodigo = unidad.GradoCodigo,
                                SectorId = unidad.SectorId,
                                UnidadId = unidad.Id,
                                ConocimientoId = conocimientoId
                            }.Save(context);
                        }
                    }

                    context.SubmitChanges();
                }

                return new RecursoCurricular.Result
                {
                    Status = "200"
                };
            }
            catch
            {
                return new RecursoCurricular.Result
                {
                    Status = "500"
                };
            }
        }
    }
}