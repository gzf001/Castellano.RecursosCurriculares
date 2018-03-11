using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Api.Areas.EducacionParvularia.Models
{
    public class PrincipioPedagogico : RecursoCurricular.Api.Models.Result
    {
        public RecursoCurricular.BaseCurricular.PrincipioPedagogico Item
        {
            get;
            set;
        }

        public List<RecursoCurricular.BaseCurricular.PrincipioPedagogico> PrincipiosPedagogicos
        {
            get;
            set;
        }
    }
}