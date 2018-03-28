using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RecursoCurricular.Api.Areas.RecursosCurriculares.Controllers
{
    public class AprendizajeIndicadorController : ApiController
    {
        [HttpGet]
        [Route("api/AprendizajeIndicador")]
        public RecursoCurricular.Api.Models.Result AprendizajeIndicador([FromUri]RecursoCurricular.Api.Areas.RecursosCurriculares.Models.Parametro parametro)
        {
            string token = this.Request.Headers.GetValues("Token").First();

            RecursoCurricular.Api.Models.TokenValido tv = new RecursoCurricular.Api.Models.TokenValido();

            RecursoCurricular.Api.Models.Result result = tv.ValidateToken(token);

            if (!result.Status.Equals("OK"))
            {
                return result;
            }

            RecursoCurricular.RecursosCurriculares.AprendizajeIndicador aprendizajeIndicador = RecursoCurricular.RecursosCurriculares.AprendizajeIndicador.Get(parametro.AnioNumero, parametro.TipoEducacionCodigo, parametro.GradoCodigo, parametro.SectorId, parametro.AprendizajeId, parametro.Id);

            return new RecursoCurricular.Api.Areas.RecursosCurriculares.Models.AprendizajeIndicador
            {
                Status = "OK",
                Message = "Correcto",
                Item = aprendizajeIndicador
            };
        }

        [HttpPost]
        [Route("api/AprendizajesIndicadores")]
        public RecursoCurricular.Api.Models.Result AprendizajesIndicadores([FromBody] RecursoCurricular.Api.Areas.RecursosCurriculares.Models.Parametro parametro)
        {
            string token = this.Request.Headers.GetValues("Token").First();

            RecursoCurricular.Api.Models.TokenValido tv = new RecursoCurricular.Api.Models.TokenValido();

            RecursoCurricular.Api.Models.Result result = tv.ValidateToken(token);

            if (!result.Status.Equals("OK"))
            {
                return result;
            }

            RecursoCurricular.RecursosCurriculares.Aprendizaje aprendizaje = RecursoCurricular.RecursosCurriculares.Aprendizaje.Get(parametro.AnioNumero, parametro.TipoEducacionCodigo, parametro.GradoCodigo, parametro.SectorId, parametro.AprendizajeId);

            List<RecursoCurricular.RecursosCurriculares.AprendizajeIndicador> aprendizajesIndicadores = RecursoCurricular.RecursosCurriculares.AprendizajeIndicador.GetAll(aprendizaje);

            return new RecursoCurricular.Api.Areas.RecursosCurriculares.Models.AprendizajeIndicador { Status = "OK", Message = "Correcto", Lista = aprendizajesIndicadores };
        }
    }
}