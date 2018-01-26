using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models
{
    public class Actitud : RecursoCurricular.BaseCurricular.Actitud
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

        public class Actitudes
        {
            public List<Actitud> data
            {
                get;
                set;
            }
        }
    }
}