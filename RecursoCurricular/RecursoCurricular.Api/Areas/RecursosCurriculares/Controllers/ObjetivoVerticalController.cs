using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RecursoCurricular.Api.Areas.RecursosCurriculares.Controllers
{
    public class ObjetivoVerticalController : ApiController
    {
        [HttpGet]
        public RecursoCurricular.Api.Models.Result ObjetivoVertical([FromUri]RecursoCurricular.Api.Areas.RecursosCurriculares.Models.Parametro parametro)
        {
            string token = this.Request.Headers.GetValues("Token").First();

            RecursoCurricular.Api.Models.TokenValido tv = new RecursoCurricular.Api.Models.TokenValido();

            RecursoCurricular.Api.Models.Result result = tv.ValidateToken(token);

            if (!result.Status.Equals("OK"))
            {
                return result;
            }

            RecursoCurricular.RecursosCurriculares.ObjetivoVertical objetivoVertical = RecursoCurricular.RecursosCurriculares.ObjetivoVertical.Get(parametro.AnioNumero, parametro.TipoEducacionCodigo, parametro.GradoCodigo, parametro.SectorId, parametro.Id);

            return new RecursoCurricular.Api.Areas.RecursosCurriculares.Models.ObjetivoVertical
            {
                Status = "OK",
                Message = "Correcto",
                Item = objetivoVertical
            };
        }

        [HttpPost]
        public RecursoCurricular.Api.Models.Result ObjetivosVerticales([FromBody] RecursoCurricular.Api.Areas.RecursosCurriculares.Models.Parametro parametro)
        {
            string token = this.Request.Headers.GetValues("Token").First();

            RecursoCurricular.Api.Models.TokenValido tv = new RecursoCurricular.Api.Models.TokenValido();

            RecursoCurricular.Api.Models.Result result = tv.ValidateToken(token);

            if (!result.Status.Equals("OK"))
            {
                return result;
            }

            RecursoCurricular.Anio anio = RecursoCurricular.Anio.Get(parametro.AnioNumero);

            RecursoCurricular.Educacion.Grado grado = RecursoCurricular.Educacion.Grado.Get(parametro.TipoEducacionCodigo, parametro.GradoCodigo);

            RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(parametro.SectorId);

            List<RecursoCurricular.RecursosCurriculares.ObjetivoVertical> objetivosVerticales = RecursoCurricular.RecursosCurriculares.ObjetivoVertical.GetAll(anio, grado, sector);

            return new RecursoCurricular.Api.Areas.RecursosCurriculares.Models.ObjetivoVertical { Status = "OK", Message = "Correcto", ObjetivosVerticales = objetivosVerticales };
        }
    }
}