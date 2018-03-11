using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Api.Areas.BasesCurriculares.Models
{
    public class ObjetivoAprendizaje : RecursoCurricular.Api.Models.Result
    {
        public RecursoCurricular.BaseCurricular.ObjetivoAprendizaje Item
        {
            get;
            set;
        }

        public List<RecursoCurricular.BaseCurricular.ObjetivoAprendizaje> ObjetivosAprendizaje
        {
            get;
            set;
        }
    }
}