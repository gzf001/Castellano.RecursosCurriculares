using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Api.Areas.RecursosCurriculares.Models
{
    public class ObjetivoVertical : RecursoCurricular.Api.Models.Result
    {
        public RecursoCurricular.RecursosCurriculares.ObjetivoVertical Item
        {
            get;
            set;
        }

        public List<RecursoCurricular.RecursosCurriculares.ObjetivoVertical> ObjetivosVerticales
        {
            get;
            set;
        }
    }
}