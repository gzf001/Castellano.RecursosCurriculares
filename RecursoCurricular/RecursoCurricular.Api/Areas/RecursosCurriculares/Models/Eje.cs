using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Api.Areas.RecursosCurriculares.Models
{
    public class Eje : RecursoCurricular.Api.Models.Result
    {
        public RecursoCurricular.RecursosCurriculares.Eje Item
        {
            get;
            set;
        }

        public List<RecursoCurricular.RecursosCurriculares.Eje> Ejes
        {
            get;
            set;
        }
    }
}