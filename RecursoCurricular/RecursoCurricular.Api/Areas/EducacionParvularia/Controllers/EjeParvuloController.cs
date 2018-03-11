using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RecursoCurricular.Api.Areas.EducacionParvularia.Controllers
{
    public class EjeParvuloController : ApiController
    {
        [HttpGet]
        public RecursoCurricular.Api.Models.Result Eje([FromUri]RecursoCurricular.Api.Areas.EducacionParvularia.Models.Parametro parametro)
        {
            string token = this.Request.Headers.GetValues("Token").First();

            RecursoCurricular.Api.Models.TokenValido tv = new RecursoCurricular.Api.Models.TokenValido();

            RecursoCurricular.Api.Models.Result result = tv.ValidateToken(token);

            if (!result.Status.Equals("OK"))
            {
                return result;
            }

            RecursoCurricular.BaseCurricular.EjeParvulo ejeParvulo = RecursoCurricular.BaseCurricular.EjeParvulo.Get(parametro.AnioNumero, parametro.AmbitoExperienciaAprendizajeCodigo, parametro.NucleoAprendizajeId, parametro.CicloCodigo, parametro.Id);

            return new RecursoCurricular.Api.Areas.EducacionParvularia.Models.Eje
            {
                Status = "OK",
                Message = "Correcto",
                Item = ejeParvulo
            };
        }

        [HttpPost]
        public RecursoCurricular.Api.Models.Result Ejes([FromBody] RecursoCurricular.Api.Areas.EducacionParvularia.Models.Parametro parametro)
        {
            string token = this.Request.Headers.GetValues("Token").First();

            RecursoCurricular.Api.Models.TokenValido tv = new RecursoCurricular.Api.Models.TokenValido();

            RecursoCurricular.Api.Models.Result result = tv.ValidateToken(token);

            if (!result.Status.Equals("OK"))
            {
                return result;
            }

            RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje ambitoExperienciaAprendizaje = RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje.Get(parametro.AnioNumero, parametro.AmbitoExperienciaAprendizajeCodigo);

            RecursoCurricular.BaseCurricular.NucleoAprendizaje nucleoAprendizaje = RecursoCurricular.BaseCurricular.NucleoAprendizaje.Get(parametro.AnioNumero, parametro.AmbitoExperienciaAprendizajeCodigo, parametro.NucleoAprendizajeId);

            RecursoCurricular.Educacion.Ciclo ciclo = RecursoCurricular.Educacion.Ciclo.Get(parametro.CicloCodigo);

            List<RecursoCurricular.BaseCurricular.EjeParvulo> ejes = RecursoCurricular.BaseCurricular.EjeParvulo.GetAll(ambitoExperienciaAprendizaje, nucleoAprendizaje, ciclo);

            return new RecursoCurricular.Api.Areas.EducacionParvularia.Models.Eje { Status = "OK", Message = "Correcto", EjesParvulo = ejes };
        }
    }
}