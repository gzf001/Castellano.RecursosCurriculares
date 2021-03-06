using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RecursoCurricular
{
	public partial class NivelEducacional : RecursoCurricular.Default
    {
        public static IEnumerable<SelectListItem> NivelesEducacionales
        {
            get
            {
                SelectList lista = new SelectList(RecursoCurricular.NivelEducacional.GetAll(), "Codigo", "Nombre");

                return DefaultItem.Concat(lista);
            }
        }

        public static NivelEducacional Get(Int32 codigo)
        {
            return Query.GetNivelEducacionales().SingleOrDefault<NivelEducacional>(x => x.Codigo == codigo);
        }

        public static List<NivelEducacional> GetAll()
        {
            return
                (
                from query in Query.GetNivelEducacionales()
                select query
                ).ToList<NivelEducacional>();
        }
    }
}