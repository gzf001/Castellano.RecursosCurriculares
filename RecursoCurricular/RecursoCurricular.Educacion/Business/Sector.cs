using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RecursoCurricular.Educacion
{
	public partial class Sector : RecursoCurricular.Default
	{
        public static IEnumerable<SelectListItem> Sectores
        {
            get
            {
                SelectList lista = new SelectList(RecursoCurricular.Educacion.Sector.GetAll(), "Id", "Nombre");

                return DefaultItem.Concat(lista);
            }
        }

        public static Sector Get(Guid id)
        {
            return Query.GetSectores().SingleOrDefault<RecursoCurricular.Educacion.Sector>(x => x.Id.Equals(id));
        }

        public static List<Sector> GetAll()
		{
			return
				(
				from query in Query.GetSectores()
                orderby query.Orden
				select query
				).ToList<Sector>();
		}
    }
}