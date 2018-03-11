using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RecursoCurricular.Api.Areas.BasesCurriculares.Controllers
{
    public class SubHabilidadController : ApiController
    {
        [HttpGet]
        public RecursoCurricular.Api.Models.Result SubHabilidad([FromUri]RecursoCurricular.Api.Areas.BasesCurriculares.Models.Parametro parametro)
        {
            string token = this.Request.Headers.GetValues("Token").First();

            RecursoCurricular.Api.Models.TokenValido tv = new RecursoCurricular.Api.Models.TokenValido();

            RecursoCurricular.Api.Models.Result result = tv.ValidateToken(token);

            if (!result.Status.Equals("OK"))
            {
                return result;
            }

            RecursoCurricular.BaseCurricular.SubHabilidad SubHabilidad = RecursoCurricular.BaseCurricular.SubHabilidad.Get(parametro.TipoEducacionCodigo, parametro.AnioNumero, parametro.GradoCodigo, parametro.HabilidadId, parametro.SectorId, parametro.Id);

            return new RecursoCurricular.Api.Areas.BasesCurriculares.Models.SubHabilidad
            {
                Status = "OK",
                Message = "Correcto",
                Item = SubHabilidad
            };
        }

        [HttpPost]
        public RecursoCurricular.Api.Models.Result SubHabilidades([FromBody] RecursoCurricular.Api.Areas.BasesCurriculares.Models.Parametro parametro)
        {
            string token = this.Request.Headers.GetValues("Token").First();

            RecursoCurricular.Api.Models.TokenValido tv = new RecursoCurricular.Api.Models.TokenValido();

            RecursoCurricular.Api.Models.Result result = tv.ValidateToken(token);

            if (!result.Status.Equals("OK"))
            {
                return result;
            }

            RecursoCurricular.BaseCurricular.Habilidad habilidad = RecursoCurricular.BaseCurricular.Habilidad.Get(parametro.HabilidadId, parametro.TipoEducacionCodigo, parametro.AnioNumero, parametro.SectorId);

            List<RecursoCurricular.BaseCurricular.SubHabilidad> SubHabilidades = RecursoCurricular.BaseCurricular.SubHabilidad.GetAll(habilidad);

            return new RecursoCurricular.Api.Areas.BasesCurriculares.Models.SubHabilidad { Status = "OK", Message = "Correcto", SubHabilidades = SubHabilidades };
        }
    }
}
