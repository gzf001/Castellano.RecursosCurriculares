using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Web.UI.Helpers.JsonClass
{
    public class Actitud : Base
    {
        public static List<Actitud> GetAll(Guid unidadId, int anioNumero, int tipoEducacionCodigo, int gradoCodigo, Guid sectorId)
        {
            RecursoCurricular.Anio anio = RecursoCurricular.Anio.Get(anioNumero);

            RecursoCurricular.Educacion.TipoEducacion tipoEducacion = RecursoCurricular.Educacion.TipoEducacion.Get(tipoEducacionCodigo);

            RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(sectorId);

            List<RecursoCurricular.Web.UI.Helpers.JsonClass.Actitud> lista = new List<RecursoCurricular.Web.UI.Helpers.JsonClass.Actitud>();

            foreach (RecursoCurricular.BaseCurricular.Actitud actitud in RecursoCurricular.BaseCurricular.Actitud.GetAll(tipoEducacion, anio, sector))
            {
                RecursoCurricular.BaseCurricular.UnidadActitud unidadActitud = RecursoCurricular.BaseCurricular.UnidadActitud.Get(tipoEducacionCodigo, anioNumero, gradoCodigo, sectorId, unidadId, actitud.Id);

                RecursoCurricular.Web.UI.Helpers.JsonClass.Actitud a = new RecursoCurricular.Web.UI.Helpers.JsonClass.Actitud
                {
                    key = actitud.Id.ToString(),
                    title = actitud.Descripcion,
                    selected = unidadActitud != null,
                    folder = false
                };

                lista.Add(a);
            }

            return lista;
        }
    }
}