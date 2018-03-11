using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Api.Areas.RecursosCurriculares.Models
{
    public class Contenido : RecursoCurricular.Api.Models.Result
    {
        public RecursoCurricular.RecursosCurriculares.Contenido Item
        {
            get;
            set;
        }

        public List<RecursoCurricular.RecursosCurriculares.Contenido> Contenidos
        {
            get;
            set;
        }
    }
}