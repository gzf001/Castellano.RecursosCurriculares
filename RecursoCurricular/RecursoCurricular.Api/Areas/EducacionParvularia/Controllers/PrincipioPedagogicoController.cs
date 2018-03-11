using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RecursoCurricular.Api.Areas.EducacionParvularia.Controllers
{
    public class PrincipioPedagogicoController : ApiController
    {
        public RecursoCurricular.Api.Models.Result PrincipiosPedagogicos([FromBody] RecursoCurricular.Api.Areas.EducacionParvularia.Models.Parametro parametro)
        {
            string token = this.Request.Headers.GetValues("Token").First();

            RecursoCurricular.Api.Models.TokenValido tv = new RecursoCurricular.Api.Models.TokenValido();

            RecursoCurricular.Api.Models.Result result = tv.ValidateToken(token);

            if (!result.Status.Equals("OK"))
            {
                return result;
            }

            RecursoCurricular.Anio anio = RecursoCurricular.Anio.Get(parametro.AnioNumero);

            List<RecursoCurricular.BaseCurricular.PrincipioPedagogico> principiosPedagogicos = RecursoCurricular.BaseCurricular.PrincipioPedagogico.GetAll(anio);

            return new RecursoCurricular.Api.Areas.EducacionParvularia.Models.PrincipioPedagogico { Status = "OK", Message = "Correcto", PrincipiosPedagogicos = principiosPedagogicos };
        }
    }
}