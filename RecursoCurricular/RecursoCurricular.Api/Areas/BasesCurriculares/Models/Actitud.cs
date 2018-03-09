using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Api.Areas.BasesCurriculares.Models
{
    public class Actitud : RecursoCurricular.Api.Models.Result
    {
        public List<RecursoCurricular.BaseCurricular.Actitud> Actitudes
        {
            get;
            set;
        }
    }
}