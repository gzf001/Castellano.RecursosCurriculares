using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Api.Areas.RecursosCurriculares.Models
{
    public class AprendizajeEsperado : RecursoCurricular.Api.Models.Result
    {
        public RecursoCurricular.RecursosCurriculares.Aprendizaje Item
        {
            get;
            set;
        }

        public List<RecursoCurricular.RecursosCurriculares.Aprendizaje> Aprendizajes
        {
            get;
            set;
        }
    }
}