using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RecursoCurricular.Api.Areas.BasesCurriculares.Controllers
{
    public class DimensionOATController : ApiController
    {
        [HttpGet]
        public RecursoCurricular.Api.Models.Result DimensionOAT([FromUri]RecursoCurricular.Api.Areas.BasesCurriculares.Models.Parametro parametro)
        {
            string token = this.Request.Headers.GetValues("Token").First();

            RecursoCurricular.Api.Models.TokenValido tv = new RecursoCurricular.Api.Models.TokenValido();

            RecursoCurricular.Api.Models.Result result = tv.ValidateToken(token);

            if (!result.Status.Equals("OK"))
            {
                return result;
            }

            RecursoCurricular.BaseCurricular.DimensionOAT dimensionesOAT = RecursoCurricular.BaseCurricular.DimensionOAT.Get(parametro.Id, parametro.AnioNumero);

            return new RecursoCurricular.Api.Areas.BasesCurriculares.Models.DimensionOAT
            {
                Status = "OK",
                Message = "Correcto",
                Item = dimensionesOAT
            };
        }

        [HttpPost]
        public RecursoCurricular.Api.Models.Result Actitudes([FromBody] RecursoCurricular.Api.Areas.BasesCurriculares.Models.Parametro parametro)
        {
            string token = this.Request.Headers.GetValues("Token").First();

            RecursoCurricular.Api.Models.TokenValido tv = new RecursoCurricular.Api.Models.TokenValido();

            RecursoCurricular.Api.Models.Result result = tv.ValidateToken(token);

            if (!result.Status.Equals("OK"))
            {
                return result;
            }

            RecursoCurricular.Anio anio = RecursoCurricular.Anio.Get(parametro.AnioNumero);

            List<RecursoCurricular.BaseCurricular.DimensionOAT> dimensionesOAT = RecursoCurricular.BaseCurricular.DimensionOAT.GetAll(anio);

            return new RecursoCurricular.Api.Areas.BasesCurriculares.Models.DimensionOAT { Status = "OK", Message = "Correcto", DimensionesOAT = dimensionesOAT };
        }
    }
}