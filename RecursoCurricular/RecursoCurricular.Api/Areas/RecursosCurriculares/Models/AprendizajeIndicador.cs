using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Api.Areas.RecursosCurriculares.Models
{
    public class AprendizajeIndicador : RecursoCurricular.Api.Models.Result
    {
        public RecursoCurricular.RecursosCurriculares.AprendizajeIndicador Item
        {
            get;
            set;
        }

        public List<RecursoCurricular.RecursosCurriculares.AprendizajeIndicador> Indicadores
        {
            get;
            set;
        }
    }
}