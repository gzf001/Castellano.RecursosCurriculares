using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Web.UI.Helpers.JsonClass
{
    public class Aprendizaje : Base
    {
        public List<Contenido> children
        {
            get;
            set;
        }

        public static List<Aprendizaje> GetAll(int anioNumero, int tipoEducacionCodigo, int gradoCodigo, Guid sectorId, Guid unidadId)
        {
            RecursoCurricular.Anio anio = RecursoCurricular.Anio.Get(anioNumero);

            RecursoCurricular.Educacion.Grado grado = RecursoCurricular.Educacion.Grado.Get(tipoEducacionCodigo, gradoCodigo);

            RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(sectorId);

            List<RecursoCurricular.Web.UI.Helpers.JsonClass.Aprendizaje> lista = new List<RecursoCurricular.Web.UI.Helpers.JsonClass.Aprendizaje>();

            foreach (RecursoCurricular.RecursosCurriculares.Aprendizaje aprendizaje in RecursoCurricular.RecursosCurriculares.Aprendizaje.GetAll(anio, grado, sector))
            {
                RecursoCurricular.RecursosCurriculares.UnidadAprendizaje unidadAprendizaje = RecursoCurricular.RecursosCurriculares.UnidadAprendizaje.Get(anioNumero, tipoEducacionCodigo, gradoCodigo, sectorId, unidadId, aprendizaje.Id);

                RecursoCurricular.Web.UI.Helpers.JsonClass.Aprendizaje a = new RecursoCurricular.Web.UI.Helpers.JsonClass.Aprendizaje
                {
                    key = aprendizaje.Id.ToString(),
                    title = aprendizaje.Descripcion,
                    selected = unidadAprendizaje != null,
                    folder = false
                };

                lista.Add(a);
            }

            return lista;
        }
    }
}