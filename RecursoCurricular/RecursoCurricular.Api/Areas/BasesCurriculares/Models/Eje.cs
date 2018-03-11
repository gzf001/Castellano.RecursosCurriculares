using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Api.Areas.BasesCurriculares.Models
{
    public class Eje : RecursoCurricular.Api.Models.Result
    {
        public RecursoCurricular.BaseCurricular.Eje Item
        {
            get;
            set;
        }

        public List<RecursoCurricular.BaseCurricular.Eje> Ejes
        {
            get;
            set;
        }
    }
}