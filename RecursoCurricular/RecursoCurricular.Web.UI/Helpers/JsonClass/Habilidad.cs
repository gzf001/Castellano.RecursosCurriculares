using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Web.UI.Helpers.JsonClass
{
    public class Habilidad : Base
    {
        public List<Habilidad> children
        {
            get;
            set;
        }

        public static List<Habilidad> GetAll(Guid unidadId, int anioNumero, int tipoEducacionCodigo, int gradoCodigo, Guid sectorId)
        {
            RecursoCurricular.Anio anio = RecursoCurricular.Anio.Get(anioNumero);

            RecursoCurricular.Educacion.Grado grado = RecursoCurricular.Educacion.Grado.Get(tipoEducacionCodigo, gradoCodigo);

            RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(sectorId);

            RecursoCurricular.BaseCurricular.Unidad unidad = RecursoCurricular.BaseCurricular.Unidad.Get(tipoEducacionCodigo, anioNumero, gradoCodigo, sectorId, unidadId);

            List<Habilidad> lista = new List<Habilidad>();

            foreach (RecursoCurricular.BaseCurricular.Habilidad habilidad in RecursoCurricular.BaseCurricular.Habilidad.GetAll(anio, grado.TipoEducacion, sector))
            {
                RecursoCurricular.Web.UI.Helpers.JsonClass.Habilidad h = new RecursoCurricular.Web.UI.Helpers.JsonClass.Habilidad
                {
                    key = "padre",
                    title = habilidad.Descripcion,
                    folder = true,
                    children = new List<RecursoCurricular.Web.UI.Helpers.JsonClass.Habilidad>()
                };

                foreach (RecursoCurricular.BaseCurricular.SubHabilidad subHabilidad in RecursoCurricular.BaseCurricular.SubHabilidad.GetAll(habilidad, grado))
                {
                    RecursoCurricular.BaseCurricular.UnidadSubHabilidad unidadSubHabilidad = RecursoCurricular.BaseCurricular.UnidadSubHabilidad.Get(tipoEducacionCodigo, anioNumero, gradoCodigo, sectorId, unidadId, subHabilidad.HabilidadId, subHabilidad.Id);

                    RecursoCurricular.Web.UI.Helpers.JsonClass.Habilidad s = new RecursoCurricular.Web.UI.Helpers.JsonClass.Habilidad
                    {
                        key = string.Format("{0}{1}", subHabilidad.HabilidadId, subHabilidad.Id),
                        title = subHabilidad.Descripcion,
                        selected = unidadSubHabilidad != null,
                        folder = false,
                        children = new List<RecursoCurricular.Web.UI.Helpers.JsonClass.Habilidad>()
                    };

                    h.children.Add(s);
                }

                lista.Add(h);
            }

            return lista;
        }
    }
}