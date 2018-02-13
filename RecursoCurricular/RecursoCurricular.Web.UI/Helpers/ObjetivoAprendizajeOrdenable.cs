using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecursoCurricular.Web.UI.Helpers
{
    public static class ObjetivoAprendizajeOrdenable
    {
        public static MvcHtmlString SortObjetivoAprendizaje(this HtmlHelper helper, string id)
        {
            TagBuilder t = new TagBuilder("ul");

            t.Attributes.Add("id", id);

            

            return new MvcHtmlString(t.ToString(TagRenderMode.Normal));
        }
    }
}