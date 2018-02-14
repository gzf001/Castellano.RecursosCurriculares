using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecursoCurricular.Web.UI.Helpers
{
    public static class ObjetivoAprendizajeOrdenable
    {
        public static MvcHtmlString SortObjetivoAprendizaje(this HtmlHelper helper, string id, int tipoEducacionCodigo, int anioNumero, int gradoCodigo, Guid sectorId, Guid unidadId)
        {
            RecursoCurricular.BaseCurricular.Unidad unidad = RecursoCurricular.BaseCurricular.Unidad.Get(tipoEducacionCodigo, anioNumero, gradoCodigo, sectorId, unidadId);

            TagBuilder t = new TagBuilder("ul");

            t.Attributes.Add("id", id);

            t.AddCssClass("dd-list");

            foreach (RecursoCurricular.BaseCurricular.UnidadObjetivoAprendizaje unidadObjetivoAprendizaje in RecursoCurricular.BaseCurricular.UnidadObjetivoAprendizaje.GetAll(unidad))
            {
                t.InnerHtml += "<li class='dd-item'>";
                t.InnerHtml += string.Format("<div class='dd-handle'>{0}<br /><br />{1}</div>", unidadObjetivoAprendizaje.ObjetivoAprendizaje.Eje.Nombre, unidadObjetivoAprendizaje.ObjetivoAprendizaje.Descripcion);
                t.InnerHtml += "<div class='dd'>";
                t.InnerHtml += "<ul id='indicador' class='dd-list'>";

                foreach (RecursoCurricular.BaseCurricular.Indicador indicador in RecursoCurricular.BaseCurricular.Indicador.GetAll(unidadObjetivoAprendizaje.ObjetivoAprendizaje))
                {
                    t.InnerHtml += string.Format("<li class='dd-item'><div class='dd-handle'>{0}</div></li>", indicador.Descripcion);
                }

                t.InnerHtml += "</ul>";
                t.InnerHtml += "</div>";
                t.InnerHtml += "</li>";
            }

            return new MvcHtmlString(t.ToString(TagRenderMode.Normal));
        }
    }
}