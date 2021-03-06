using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RecursoCurricular.Educacion
{
	public partial class Ciclo : RecursoCurricular.Default
	{
        public static IEnumerable<SelectListItem> Ciclos
        {
            get
            {
                SelectList lista = new SelectList(RecursoCurricular.Educacion.Ciclo.GetAll(), "Codigo", "Nombre");

                return DefaultItem.Concat(lista);
            }
        }

        public static Ciclo Get(int codigo)
        {
            return Query.GetCiclos().SingleOrDefault<RecursoCurricular.Educacion.Ciclo>(x => x.Codigo.Equals(codigo));
        }

        public static List<Ciclo> GetAll()
		{
			return
				(
				from query in Query.GetCiclos()
				select query
				).ToList<Ciclo>();
		}
    }
}