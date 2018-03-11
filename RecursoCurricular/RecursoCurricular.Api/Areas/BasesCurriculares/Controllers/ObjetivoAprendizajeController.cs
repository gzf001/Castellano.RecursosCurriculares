using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RecursoCurricular.Api.Areas.BasesCurriculares.Controllers
{
    public class ObjetivoAprendizajeController : ApiController
    {
        [HttpGet]
        public RecursoCurricular.Api.Models.Result ObjetivoAprendizaje([FromUri]RecursoCurricular.Api.Areas.BasesCurriculares.Models.Parametro parametro)
        {
            string token = this.Request.Headers.GetValues("Token").First();

            RecursoCurricular.Api.Models.TokenValido tv = new RecursoCurricular.Api.Models.TokenValido();

            RecursoCurricular.Api.Models.Result result = tv.ValidateToken(token);

            if (!result.Status.Equals("OK"))
            {
                return result;
            }

            RecursoCurricular.BaseCurricular.ObjetivoAprendizaje objetivoAprendizaje = RecursoCurricular.BaseCurricular.ObjetivoAprendizaje.Get(parametro.TipoEducacionCodigo, parametro.AnioNumero, parametro.GradoCodigo, parametro.SectorId, parametro.EjeId, parametro.Id);

            return new RecursoCurricular.Api.Areas.BasesCurriculares.Models.ObjetivoAprendizaje
            {
                Status = "OK",
                Message = "Correcto",
                Item = objetivoAprendizaje
            };
        }

        [HttpPost]
        public RecursoCurricular.Api.Models.Result ObjetivosAprendizaje([FromBody] RecursoCurricular.Api.Areas.BasesCurriculares.Models.Parametro parametro)
        {
            string token = this.Request.Headers.GetValues("Token").First();

            RecursoCurricular.Api.Models.TokenValido tv = new RecursoCurricular.Api.Models.TokenValido();

            RecursoCurricular.Api.Models.Result result = tv.ValidateToken(token);

            if (!result.Status.Equals("OK"))
            {
                return result;
            }

            RecursoCurricular.Educacion.Grado grado = RecursoCurricular.Educacion.Grado.Get(parametro.TipoEducacionCodigo, parametro.GradoCodigo);

            RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(parametro.SectorId);

            RecursoCurricular.BaseCurricular.Eje eje = RecursoCurricular.BaseCurricular.Eje.Get(parametro.AnioNumero, parametro.SectorId, parametro.EjeId);

            List<RecursoCurricular.BaseCurricular.ObjetivoAprendizaje> objetivosAprendizaje = RecursoCurricular.BaseCurricular.ObjetivoAprendizaje.GetAll(grado, sector, eje);

            return new RecursoCurricular.Api.Areas.BasesCurriculares.Models.ObjetivoAprendizaje { Status = "OK", Message = "Correcto", ObjetivosAprendizaje = objetivosAprendizaje };
        }
    }
}
