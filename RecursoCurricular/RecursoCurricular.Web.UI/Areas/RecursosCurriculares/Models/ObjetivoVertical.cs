using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models
{
    public class ObjetivoVertical : RecursoCurricular.RecursosCurriculares.ObjetivoVertical
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

        public string GradoNombre
        {
            get;
            set;
        }

        public string SectorNombre
        {
            get;
            set;
        }

        public class ObjetivosVerticales
        {
            public List<ObjetivoVertical> data
            {
                get;
                set;
            }
        }
    }
}