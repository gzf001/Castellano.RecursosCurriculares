using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RecursoCurricular.Educacion
{
	public partial class TipoEducacion : RecursoCurricular.Default
    {
        public static IEnumerable<SelectListItem> TiposEducacion
        {
            get
            {
                SelectList lista = new SelectList(RecursoCurricular.Educacion.TipoEducacion.GetAll(), "Codigo", "Nombre");

                return DefaultItem.Concat(lista);
            }
        }

        public static List<TipoEducacion> GetAll()
		{
			return
				(
				from query in Query.GetTipoEducaciones()
				select query
				).ToList<TipoEducacion>();
		}
	}
}