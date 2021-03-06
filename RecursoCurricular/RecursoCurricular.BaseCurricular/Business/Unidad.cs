using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
    public partial class Unidad
    {
        const string urlSincronizacion = "/api/BasesCurriculares/UnidadesBaseCurricular";

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

                    unidad.SyncUp();
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

        public void SyncUp()
        {
            RecursoCurricular.BaseCurricular.PassingObject.Unidad unidad = new RecursoCurricular.BaseCurricular.PassingObject.Unidad
            {
                TipoEducacionCodigo = this.TipoEducacionCodigo,
                AnoNumero = this.AnoNumero,
                GradoCodigo = this.GradoCodigo,
                SectorId = this.SectorId,
                Id = this.Id,
                AmbitoCodigo = 1,
                SostenedorId = default(Guid),
                EstablecimientoId = default(Guid),
                Proposito = this.Proposito,
                ConocimientoPrevio = this.ConocimientoPrevio,
                PalabraClave = this.PalabraClave,
                Numero = this.Numero,
                Nombre = this.Nombre,
                Habilidades = new List<RecursoCurricular.BaseCurricular.PassingObject.Habilidad>(),
                Ejes = new List<RecursoCurricular.BaseCurricular.PassingObject.Eje>(),
                Actitudes = new List<RecursoCurricular.BaseCurricular.PassingObject.Actitud>(),
                Conocimientos = new List<RecursoCurricular.BaseCurricular.PassingObject.Conocimiento>(),
                UrlSincronizacion = urlSincronizacion
            };

            List<RecursoCurricular.BaseCurricular.UnidadObjetivoAprendizaje> unidadObjetivosAprendizaje = RecursoCurricular.BaseCurricular.UnidadObjetivoAprendizaje.GetAll(this);

            List<RecursoCurricular.BaseCurricular.UnidadIndicador> unidadIndicadores = RecursoCurricular.BaseCurricular.UnidadIndicador.GetAll(this);

            List<RecursoCurricular.BaseCurricular.UnidadSubHabilidad> unidadSubHabilidades = RecursoCurricular.BaseCurricular.UnidadSubHabilidad.GetAll(this);

            List<RecursoCurricular.BaseCurricular.UnidadActitud> unidadActitudes = RecursoCurricular.BaseCurricular.UnidadActitud.GetAll(this);

            List<RecursoCurricular.BaseCurricular.UnidadConocimiento> unidadConocimientos = RecursoCurricular.BaseCurricular.UnidadConocimiento.GetAll(this);

            List<RecursoCurricular.BaseCurricular.Eje> ejes = new List<RecursoCurricular.BaseCurricular.Eje>();

            List<RecursoCurricular.BaseCurricular.ObjetivoAprendizaje> objetivosAprendizaje = new List<RecursoCurricular.BaseCurricular.ObjetivoAprendizaje>();

            List<RecursoCurricular.BaseCurricular.Indicador> indicadores = new List<RecursoCurricular.BaseCurricular.Indicador>();

            List<RecursoCurricular.BaseCurricular.Habilidad> habilidades = new List<RecursoCurricular.BaseCurricular.Habilidad>();

            List<RecursoCurricular.BaseCurricular.SubHabilidad> subHabilidades = new List<RecursoCurricular.BaseCurricular.SubHabilidad>();

            if (unidadObjetivosAprendizaje.Any<RecursoCurricular.BaseCurricular.UnidadObjetivoAprendizaje>())
            {
                ejes = unidadObjetivosAprendizaje.Select<RecursoCurricular.BaseCurricular.UnidadObjetivoAprendizaje, RecursoCurricular.BaseCurricular.Eje>(x => x.ObjetivoAprendizaje.Eje).Distinct().ToList<RecursoCurricular.BaseCurricular.Eje>();

                objetivosAprendizaje = unidadObjetivosAprendizaje.Select<RecursoCurricular.BaseCurricular.UnidadObjetivoAprendizaje, RecursoCurricular.BaseCurricular.ObjetivoAprendizaje>(x => x.ObjetivoAprendizaje).Distinct().ToList<RecursoCurricular.BaseCurricular.ObjetivoAprendizaje>();

                indicadores = unidadIndicadores.Select<RecursoCurricular.BaseCurricular.UnidadIndicador, RecursoCurricular.BaseCurricular.Indicador>(x => x.Indicador).Distinct().ToList<RecursoCurricular.BaseCurricular.Indicador>();
            }

            if (unidadSubHabilidades.Any<RecursoCurricular.BaseCurricular.UnidadSubHabilidad>())
            {
                habilidades = unidadSubHabilidades.Select<RecursoCurricular.BaseCurricular.UnidadSubHabilidad, RecursoCurricular.BaseCurricular.Habilidad>(x => x.SubHabilidad.Habilidad).Distinct().ToList<RecursoCurricular.BaseCurricular.Habilidad>();

                subHabilidades = unidadSubHabilidades.Select<RecursoCurricular.BaseCurricular.UnidadSubHabilidad, RecursoCurricular.BaseCurricular.SubHabilidad>(x => x.SubHabilidad).Distinct().ToList<RecursoCurricular.BaseCurricular.SubHabilidad>();
            }

            foreach (RecursoCurricular.BaseCurricular.Eje eje in ejes)
            {
                RecursoCurricular.BaseCurricular.PassingObject.Eje e = new RecursoCurricular.BaseCurricular.PassingObject.Eje
                {
                    TipoEducacionCodigo = this.TipoEducacionCodigo,
                    AnoNumero = eje.AnoNumero,
                    SectorId = eje.SectorId,
                    Id = eje.Id,
                    AmbitoCodigo = 1,
                    SostenedorId = default(Guid),
                    EstablecimientoId = default(Guid),
                    Numero = eje.Numero,
                    Nombre = eje.Nombre,
                    ObjetivosAprendizaje = new List<RecursoCurricular.BaseCurricular.PassingObject.ObjetivoAprendizaje>()
                };

                foreach (RecursoCurricular.BaseCurricular.ObjetivoAprendizaje objetivoAprendizaje in objetivosAprendizaje.FindAll(x => x.Eje.Equals(eje)))
                {
                    RecursoCurricular.BaseCurricular.PassingObject.ObjetivoAprendizaje o = new RecursoCurricular.BaseCurricular.PassingObject.ObjetivoAprendizaje
                    {
                        TipoEducacionCodigo = objetivoAprendizaje.TipoEducacionCodigo,
                        AnoNumero = objetivoAprendizaje.AnoNumero,
                        GradoCodigo = objetivoAprendizaje.GradoCodigo,
                        SectorId = objetivoAprendizaje.SectorId,
                        EjeId = objetivoAprendizaje.EjeId,
                        Id = objetivoAprendizaje.Id,
                        Numero = objetivoAprendizaje.Numero,
                        AmbitoCodigo = 1,
                        SostenedorId = default(Guid),
                        EstablecimientoId = default(Guid),
                        Descripcion = objetivoAprendizaje.Descripcion,
                        Indicadores = new List<RecursoCurricular.BaseCurricular.PassingObject.Indicador>()
                    };

                    foreach (RecursoCurricular.BaseCurricular.Indicador indicador in indicadores.FindAll(x => x.ObjetivoAprendizaje.Equals(objetivoAprendizaje)))
                    {
                        o.Indicadores.Add(new RecursoCurricular.BaseCurricular.PassingObject.Indicador
                        {
                            TipoEducacionCodigo = indicador.TipoEducacionCodigo,
                            AnoNumero = indicador.AnoNumero,
                            GradoCodigo = indicador.GradoCodigo,
                            SectorId = indicador.SectorId,
                            EjeId = indicador.EjeId,
                            ObjetivoAprendizajeId = indicador.ObjetivoAprendizajeId,
                            Id = indicador.Id,
                            Numero = indicador.Numero,
                            AmbitoCodigo = 1,
                            SostenedorId = default(Guid),
                            EstablecimientoId = default(Guid),
                            Descripcion = indicador.Descripcion
                        });
                    }

                    e.ObjetivosAprendizaje.Add(o);
                }

                unidad.Ejes.Add(e);
            }

            foreach (RecursoCurricular.BaseCurricular.Habilidad habilidad in habilidades)
            {
                RecursoCurricular.BaseCurricular.PassingObject.Habilidad h = new RecursoCurricular.BaseCurricular.PassingObject.Habilidad
                {
                    Id = habilidad.Id,
                    TipoEducacionCodigo = habilidad.TipoEducacionCodigo,
                    AnoNumero = habilidad.AnoNumero,
                    SectorId = habilidad.SectorId,
                    Numero = habilidad.Numero,
                    AmbitoCodigo = 1,
                    SostenedorId = default(Guid),
                    EstablecimientoId = default(Guid),
                    Descripcion = habilidad.Descripcion,
                    SubHabilidades = new List<RecursoCurricular.BaseCurricular.PassingObject.SubHabilidad>()
                };

                foreach (RecursoCurricular.BaseCurricular.SubHabilidad subHabilidad in subHabilidades)
                {
                    h.SubHabilidades.Add(new RecursoCurricular.BaseCurricular.PassingObject.SubHabilidad
                    {
                        TipoEducacionCodigo = subHabilidad.TipoEducacionCodigo,
                        AnoNumero = subHabilidad.AnoNumero,
                        GradoCodigo = subHabilidad.GradoCodigo,
                        HabilidadId = subHabilidad.HabilidadId,
                        SectorId = subHabilidad.SectorId,
                        Id = subHabilidad.Id,
                        Numero = subHabilidad.Numero,
                        AmbitoCodigo = 1,
                        SostenedorId = default(Guid),
                        EstablecimientoId = default(Guid),
                        Descripcion = subHabilidad.Descripcion
                    });
                }

                unidad.Habilidades.Add(h);
            }

            foreach (RecursoCurricular.BaseCurricular.UnidadActitud unidadActitud in unidadActitudes)
            {
                unidad.Actitudes.Add(new RecursoCurricular.BaseCurricular.PassingObject.Actitud
                {
                    TipoEducacionCodigo = unidadActitud.TipoEducacionCodigo,
                    AnoNumero = unidadActitud.AnoNumero,
                    SectorId = unidadActitud.SectorId,
                    Id = unidadActitud.Actitud.Id,
                    AmbitoCodigo = 1,
                    SostenedorId = default(Guid),
                    EstablecimientoId = default(Guid),
                    Numero = unidadActitud.Actitud.Numero,
                    Nombre = unidadActitud.Actitud.Nombre,
                    Descripcion = unidadActitud.Actitud.Descripcion
                });
            }

            foreach (RecursoCurricular.BaseCurricular.UnidadConocimiento unidadConocimiento in unidadConocimientos)
            {
                unidad.Conocimientos.Add(new RecursoCurricular.BaseCurricular.PassingObject.Conocimiento
                {
                    TipoEducacionCodigo = unidadConocimiento.TipoEducacionCodigo,
                    AnoNumero = unidadConocimiento.AnoNumero,
                    SectorId = unidadConocimiento.SectorId,
                    Id = unidadConocimiento.Conocimiento.Id,
                    AmbitoCodigo = 1,
                    SostenedorId = default(Guid),
                    EstablecimientoId = default(Guid),
                    Numero = unidadConocimiento.Conocimiento.Numero,
                    Nombre = unidadConocimiento.Conocimiento.Nombre,
                    Descripcion = unidadConocimiento.Conocimiento.Descripcion
                });
            }

            RecursoCurricular.Synchronization synchronization = new RecursoCurricular.Synchronization();

            synchronization.SyncUp(unidad);
        }
    }
}