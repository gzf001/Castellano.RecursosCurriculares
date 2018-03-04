using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models
{
    public class Unidad : RecursoCurricular.RecursosCurriculares.Unidad
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

        public List<RecursoCurricular.RecursosCurriculares.Aprendizaje> Aprendizajes
        {
            get;
            set;
        }

        public class Unidades
        {
            public List<Unidad> data
            {
                get;
                set;
            }
        }
    }
}