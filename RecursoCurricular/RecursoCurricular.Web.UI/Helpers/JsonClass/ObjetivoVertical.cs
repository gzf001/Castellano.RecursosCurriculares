using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Web.UI.Helpers.JsonClass
{
    public class ObjetivoVertical : Base
    {
        public static List<ObjetivoVertical> GetAll(int anioNumero, int tipoEducacionCodigo, int gradoCodigo, Guid sectorId, Guid aprendizajeId)
        {
            RecursoCurricular.Anio anio = RecursoCurricular.Anio.Get(anioNumero);

            RecursoCurricular.Educacion.Grado grado = RecursoCurricular.Educacion.Grado.Get(tipoEducacionCodigo, gradoCodigo);

            RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(sectorId);

            List<RecursoCurricular.Web.UI.Helpers.JsonClass.ObjetivoVertical> lista = new List<RecursoCurricular.Web.UI.Helpers.JsonClass.ObjetivoVertical>();

            foreach (RecursoCurricular.RecursosCurriculares.ObjetivoVertical objetivoVertical in RecursoCurricular.RecursosCurriculares.ObjetivoVertical.GetAll(anio, grado, sector))
            {
                RecursoCurricular.RecursosCurriculares.AprendizajeObjetivoVertical aprendizajeObjetivoVertical = RecursoCurricular.RecursosCurriculares.AprendizajeObjetivoVertical.Get(anioNumero, tipoEducacionCodigo, gradoCodigo, sectorId, aprendizajeId, objetivoVertical.Id);

                RecursoCurricular.Web.UI.Helpers.JsonClass.ObjetivoVertical o = new RecursoCurricular.Web.UI.Helpers.JsonClass.ObjetivoVertical
                {
                    key = objetivoVertical.Id.ToString(),
                    title = objetivoVertical.Descripcion,
                    selected = aprendizajeObjetivoVertical != null,
                    folder = false
                };

                lista.Add(o);
            }

            return lista;
        }
    }
}