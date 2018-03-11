using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RecursoCurricular.Api.Areas.EducacionParvularia.Controllers
{
    public class NucleoController : ApiController
    {
        [HttpGet]
        public RecursoCurricular.Api.Models.Result Nucleo([FromUri]RecursoCurricular.Api.Areas.EducacionParvularia.Models.Parametro parametro)
        {
            string token = this.Request.Headers.GetValues("Token").First();

            RecursoCurricular.Api.Models.TokenValido tv = new RecursoCurricular.Api.Models.TokenValido();

            RecursoCurricular.Api.Models.Result result = tv.ValidateToken(token);

            if (!result.Status.Equals("OK"))
            {
                return result;
            }

            RecursoCurricular.BaseCurricular.NucleoAprendizaje nucleoAprendizaje = RecursoCurricular.BaseCurricular.NucleoAprendizaje.Get(parametro.AnioNumero, parametro.AmbitoExperienciaAprendizajeCodigo, parametro.Id);

            return new RecursoCurricular.Api.Areas.EducacionParvularia.Models.Nucleo
            {
                Status = "OK",
                Message = "Correcto",
                Item = nucleoAprendizaje
            };
        }

        [HttpPost]
        public RecursoCurricular.Api.Models.Result Nucleos([FromBody] RecursoCurricular.Api.Areas.EducacionParvularia.Models.Parametro parametro)
        {
            string token = this.Request.Headers.GetValues("Token").First();

            RecursoCurricular.Api.Models.TokenValido tv = new RecursoCurricular.Api.Models.TokenValido();

            RecursoCurricular.Api.Models.Result result = tv.ValidateToken(token);

            if (!result.Status.Equals("OK"))
            {
                return result;
            }

            RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje ambitoExperienciaAprendizaje = RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje.Get(parametro.AnioNumero, parametro.AmbitoExperienciaAprendizajeCodigo);

            List<RecursoCurricular.BaseCurricular.NucleoAprendizaje> nucleosAprendizaje = RecursoCurricular.BaseCurricular.NucleoAprendizaje.GetAll(ambitoExperienciaAprendizaje);

            return new RecursoCurricular.Api.Areas.EducacionParvularia.Models.Nucleo { Status = "OK", Message = "Correcto", NucleosAprendizaje = nucleosAprendizaje };
        }
    }
}