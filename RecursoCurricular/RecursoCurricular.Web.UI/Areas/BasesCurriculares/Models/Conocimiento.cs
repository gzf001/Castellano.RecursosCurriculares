using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models
{
    public class Conocimiento : RecursoCurricular.BaseCurricular.Conocimiento
    {
        public string Accion
        {
            get;
            set;
        }

        public string TipoEducacionNombre
        {
            get;
            set;
        }

        public string SectorNombre
        {
            get;
            set;
        }

        public class Conocimientos
        {
            public List<Conocimiento> data
            {
                get;
                set;
            }
        }
    }
}