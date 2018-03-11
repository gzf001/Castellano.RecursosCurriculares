using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RecursoCurricular.Api.Areas.RecursosCurriculares.Controllers
{
    public class EjeController : ApiController
    {
        [HttpGet]
        public RecursoCurricular.Api.Models.Result Eje([FromUri]RecursoCurricular.Api.Areas.RecursosCurriculares.Models.Parametro parametro)
        {
            string token = this.Request.Headers.GetValues("Token").First();

            RecursoCurricular.Api.Models.TokenValido tv = new RecursoCurricular.Api.Models.TokenValido();

            RecursoCurricular.Api.Models.Result result = tv.ValidateToken(token);

            if (!result.Status.Equals("OK"))
            {
                return result;
            }

            RecursoCurricular.RecursosCurriculares.Eje eje = RecursoCurricular.RecursosCurriculares.Eje.Get(parametro.AnioNumero, parametro.SectorId, parametro.Id);

            return new RecursoCurricular.Api.Areas.RecursosCurriculares.Models.Eje
            {
                Status = "OK",
                Message = "Correcto",
                Item = eje
            };
        }

        [HttpPost]
        public RecursoCurricular.Api.Models.Result Ejes([FromBody] RecursoCurricular.Api.Areas.RecursosCurriculares.Models.Parametro parametro)
        {
            string token = this.Request.Headers.GetValues("Token").First();

            RecursoCurricular.Api.Models.TokenValido tv = new RecursoCurricular.Api.Models.TokenValido();

            RecursoCurricular.Api.Models.Result result = tv.ValidateToken(token);

            if (!result.Status.Equals("OK"))
            {
                return result;
            }

            RecursoCurricular.Anio anio = RecursoCurricular.Anio.Get(parametro.AnioNumero);

            RecursoCurricular.Educacion.TipoEducacion tipoEducacion = RecursoCurricular.Educacion.TipoEducacion.Get(parametro.TipoEducacionCodigo);

            RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(parametro.SectorId);

            List<RecursoCurricular.RecursosCurriculares.Eje> ejes = RecursoCurricular.RecursosCurriculares.Eje.GetAll(anio, sector, tipoEducacion);

            return new RecursoCurricular.Api.Areas.RecursosCurriculares.Models.Eje { Status = "OK", Message = "Correcto", Ejes = ejes };
        }
    }
}