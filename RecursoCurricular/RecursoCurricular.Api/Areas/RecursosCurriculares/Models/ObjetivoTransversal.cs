using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Api.Areas.RecursosCurriculares.Models
{
    public class ObjetivoTransversal : RecursoCurricular.Api.Models.Result
    {
        public RecursoCurricular.RecursosCurriculares.ObjetivoTransversal Item
        {
            get;
            set;
        }

        public List<RecursoCurricular.RecursosCurriculares.ObjetivoTransversal> ObjetivosTransversales
        {
            get;
            set;
        }
    }
}