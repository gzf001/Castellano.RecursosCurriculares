using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models
{
    public class Contenido : RecursoCurricular.RecursosCurriculares.Contenido
    {
        public string Accion
        {
            get;
            set;
        }

        public class Contenidos
        {
            public List<Contenido> data
            {
                get;
                set;
            }
        }
    }
}