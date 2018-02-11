using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Web.UI.Helpers.JsonClass
{
    public class Conocimiento : Base
    {
        public static List<Conocimiento> GetAll(Guid unidadId, int anioNumero, int tipoEducacionCodigo, int gradoCodigo, Guid sectorId)
        {
            RecursoCurricular.Anio anio = RecursoCurricular.Anio.Get(anioNumero);

            RecursoCurricular.Educacion.TipoEducacion tipoEducacion = RecursoCurricular.Educacion.TipoEducacion.Get(tipoEducacionCodigo);

            RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(sectorId);

            List<RecursoCurricular.Web.UI.Helpers.JsonClass.Conocimiento> lista = new List<RecursoCurricular.Web.UI.Helpers.JsonClass.Conocimiento>();

            foreach (RecursoCurricular.BaseCurricular.Conocimiento conocimiento in RecursoCurricular.BaseCurricular.Conocimiento.GetAll(tipoEducacion, anio, sector))
            {
                RecursoCurricular.BaseCurricular.UnidadConocimiento unidadConocimiento = RecursoCurricular.BaseCurricular.UnidadConocimiento.Get(tipoEducacionCodigo, anioNumero, gradoCodigo, sectorId, unidadId, conocimiento.Id);

                RecursoCurricular.Web.UI.Helpers.JsonClass.Conocimiento c = new RecursoCurricular.Web.UI.Helpers.JsonClass.Conocimiento
                {
                    key = conocimiento.Id.ToString(),
                    title = conocimiento.Descripcion,
                    selected = unidadConocimiento != null,
                    folder = false
                };

                lista.Add(c);
            }

            return lista;
        }
    }
}