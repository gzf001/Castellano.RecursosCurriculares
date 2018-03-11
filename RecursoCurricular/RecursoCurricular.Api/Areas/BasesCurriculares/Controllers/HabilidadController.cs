using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RecursoCurricular.Api.Areas.BasesCurriculares.Controllers
{
    public class HabilidadController : ApiController
    {
        [HttpGet]
        public RecursoCurricular.Api.Models.Result Actitud([FromUri]RecursoCurricular.Api.Areas.BasesCurriculares.Models.Parametro parametro)
        {
            string token = this.Request.Headers.GetValues("Token").First();

            RecursoCurricular.Api.Models.TokenValido tv = new RecursoCurricular.Api.Models.TokenValido();

            RecursoCurricular.Api.Models.Result result = tv.ValidateToken(token);

            if (!result.Status.Equals("OK"))
            {
                return result;
            }

            RecursoCurricular.BaseCurricular.Habilidad habilidad = RecursoCurricular.BaseCurricular.Habilidad.Get(parametro.Id, parametro.TipoEducacionCodigo, parametro.AnioNumero, parametro.SectorId);

            return new RecursoCurricular.Api.Areas.BasesCurriculares.Models.Habilidad
            {
                Status = "OK",
                Message = "Correcto",
                Item = habilidad
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

            RecursoCurricular.Educacion.TipoEducacion tipoEducacion = RecursoCurricular.Educacion.TipoEducacion.Get(parametro.TipoEducacionCodigo);

            RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(parametro.SectorId);

            List<RecursoCurricular.BaseCurricular.Habilidad> habilidades = RecursoCurricular.BaseCurricular.Habilidad.GetAll(anio, tipoEducacion, sector);

            return new RecursoCurricular.Api.Areas.BasesCurriculares.Models.Habilidad { Status = "OK", Message = "Correcto", Habilidades = habilidades };
        }
    }
}
