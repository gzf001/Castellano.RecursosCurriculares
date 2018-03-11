using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Api.Areas.RecursosCurriculares.Models
{
    public class Unidad : RecursoCurricular.Api.Models.Result
    {
        public RecursoCurricular.RecursosCurriculares.Unidad Item
        {
            get;
            set;
        }

        public List<RecursoCurricular.RecursosCurriculares.Unidad> Unidades
        {
            get;
            set;
        }
    }
}