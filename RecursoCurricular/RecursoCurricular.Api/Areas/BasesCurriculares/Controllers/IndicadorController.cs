using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RecursoCurricular.Api.Areas.BasesCurriculares.Controllers
{
    public class IndicadorController : ApiController
    {
        [HttpGet]
        public RecursoCurricular.Api.Models.Result Indicador([FromUri]RecursoCurricular.Api.Areas.BasesCurriculares.Models.Parametro parametro)
        {
            string token = this.Request.Headers.GetValues("Token").First();

            RecursoCurricular.Api.Models.TokenValido tv = new RecursoCurricular.Api.Models.TokenValido();

            RecursoCurricular.Api.Models.Result result = tv.ValidateToken(token);

            if (!result.Status.Equals("OK"))
            {
                return result;
            }

            RecursoCurricular.BaseCurricular.Indicador indicador = RecursoCurricular.BaseCurricular.Indicador.Get(parametro.TipoEducacionCodigo, parametro.AnioNumero, parametro.GradoCodigo, parametro.SectorId, parametro.EjeId, parametro.ObjetivoAprendizajeId, parametro.Id);

            return new RecursoCurricular.Api.Areas.BasesCurriculares.Models.Indicador
            {
                Status = "OK",
                Message = "Correcto",
                Item = indicador
            };
        }

        [HttpPost]
        public RecursoCurricular.Api.Models.Result Indicadores([FromBody] RecursoCurricular.Api.Areas.BasesCurriculares.Models.Parametro parametro)
        {
            string token = this.Request.Headers.GetValues("Token").First();

            RecursoCurricular.Api.Models.TokenValido tv = new RecursoCurricular.Api.Models.TokenValido();

            RecursoCurricular.Api.Models.Result result = tv.ValidateToken(token);

            if (!result.Status.Equals("OK"))
            {
                return result;
            }

            RecursoCurricular.BaseCurricular.ObjetivoAprendizaje objetivoAprendizaje = RecursoCurricular.BaseCurricular.ObjetivoAprendizaje.Get(parametro.TipoEducacionCodigo, parametro.AnioNumero, parametro.GradoCodigo, parametro.SectorId, parametro.EjeId, parametro.ObjetivoAprendizajeId);

            List<RecursoCurricular.BaseCurricular.Indicador> indicadores = RecursoCurricular.BaseCurricular.Indicador.GetAll(objetivoAprendizaje);

            return new RecursoCurricular.Api.Areas.BasesCurriculares.Models.Indicador { Status = "OK", Message = "Correcto", Indicadores = indicadores };
        }
    }
}