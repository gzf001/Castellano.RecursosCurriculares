using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RecursoCurricular
{
	public partial class Nacionalidad : RecursoCurricular.Default
	{
        public static IEnumerable<SelectListItem> Nacionalidades
        {
            get
            {
                SelectList lista = new SelectList(RecursoCurricular.Nacionalidad.GetAll(), "Codigo", "Nombre");

                return DefaultItem.Concat(lista);
            }
        }

        public static Nacionalidad Chilena
        {
            get
            {
                return RecursoCurricular.Nacionalidad.Get(1);
            }
        }

        public static Nacionalidad Get(Int16 codigo)
        {
            return Query.GetNacionalidades().SingleOrDefault<Nacionalidad>(x => x.Codigo == codigo);
        }

        public static List<Nacionalidad> GetAll()
        {
            return
                (
                from query in Query.GetNacionalidades()
                select query
                ).ToList<Nacionalidad>();
        }
    }
}