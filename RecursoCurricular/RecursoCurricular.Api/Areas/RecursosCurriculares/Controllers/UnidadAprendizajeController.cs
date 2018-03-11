using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RecursoCurricular.Api.Areas.RecursosCurriculares.Controllers
{
    public class UnidadAprendizajeController : ApiController
    {
        [HttpGet]
        public RecursoCurricular.Api.Models.Result Unidad([FromUri]RecursoCurricular.Api.Areas.RecursosCurriculares.Models.Parametro parametro)
        {
            string token = this.Request.Headers.GetValues("Token").First();

            RecursoCurricular.Api.Models.TokenValido tv = new RecursoCurricular.Api.Models.TokenValido();

            RecursoCurricular.Api.Models.Result result = tv.ValidateToken(token);

            if (!result.Status.Equals("OK"))
            {
                return result;
            }

            RecursoCurricular.RecursosCurriculares.Unidad Unidad = RecursoCurricular.RecursosCurriculares.Unidad.Get(parametro.TipoEducacionCodigo, parametro.AnioNumero, parametro.GradoCodigo, parametro.SectorId, parametro.Id);

            return new RecursoCurricular.Api.Areas.RecursosCurriculares.Models.Unidad
            {
                Status = "OK",
                Message = "Correcto",
                Item = Unidad
            };
        }

        [HttpPost]
        public RecursoCurricular.Api.Models.Result Unidades([FromBody] RecursoCurricular.Api.Areas.RecursosCurriculares.Models.Parametro parametro)
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

            List<RecursoCurricular.RecursosCurriculares.Unidad> unidades = RecursoCurricular.RecursosCurriculares.Unidad.GetAll(anio, grado, sector);

            return new RecursoCurricular.Api.Areas.RecursosCurriculares.Models.Unidad { Status = "OK", Message = "Correcto", Unidades = unidades };
        }
    }
}