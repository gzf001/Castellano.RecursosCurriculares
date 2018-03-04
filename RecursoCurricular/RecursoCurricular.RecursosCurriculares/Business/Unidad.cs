using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.RecursosCurriculares
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
            return Query.GetUnidades().SingleOrDefault<RecursoCurricular.RecursosCurriculares.Unidad>(x => x.TipoEducacionCodigo.Equals(tipoEducacionCodigo) && x.AnoNumero.Equals(anioNumero) && x.GradoCodigo.Equals(gradoCodigo) && x.SectorId.Equals(SectorId) && x.Id.Equals(id));
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

        public static Result Save(RecursoCurricular.RecursosCurriculares.Unidad unidad, List<RecursoCurricular.RecursosCurriculares.Aprendizaje> aprendizajes)
        {
            try
            {
                using (RecursoCurricular.RecursosCurriculares.Context context = new RecursoCurricular.RecursosCurriculares.Context())
                {
                    foreach (RecursoCurricular.RecursosCurriculares.UnidadAprendizaje unidadAprendizaje in RecursoCurricular.RecursosCurriculares.UnidadAprendizaje.GetAll(unidad))
                    {
                        unidadAprendizaje.Delete(context);
                    }

                    context.SubmitChanges();
                }

                using (RecursoCurricular.RecursosCurriculares.Context context = new RecursoCurricular.RecursosCurriculares.Context())
                {
                    new RecursoCurricular.RecursosCurriculares.Unidad
                    {
                        AnoNumero = unidad.AnoNumero,
                        TipoEducacionCodigo = unidad.TipoEducacionCodigo,
                        GradoCodigo = unidad.GradoCodigo,
                        SectorId = unidad.SectorId,
                        Id = unidad.Id,
                        Numero = unidad.Numero,
                        Nombre = unidad.Nombre.Trim()
                    }.Save(context);

                    if (aprendizajes != null)
                    {
                        foreach (RecursoCurricular.RecursosCurriculares.Aprendizaje aprendizaje in aprendizajes)
                        {
                            new RecursoCurricular.RecursosCurriculares.UnidadAprendizaje
                            {
                                AnoNumero = unidad.AnoNumero,
                                TipoEducacionCodigo = unidad.TipoEducacionCodigo,
                                GradoCodigo = unidad.GradoCodigo,
                                SectorId = unidad.SectorId,
                                UnidadId = unidad.Id,
                                AprendizajeId = aprendizaje.Id
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