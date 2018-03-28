using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RecursoCurricular.Api.Areas.RecursosCurriculares.Controllers
{
    public class AprendizajeEsperadoController : ApiController
    {
        [HttpGet]
        [Route("api/AprendizajeEsperado")]
        public RecursoCurricular.Api.Models.Result AprendizajeEsperado([FromUri]RecursoCurricular.Api.Areas.RecursosCurriculares.Models.Parametro parametro)
        {
            string token = this.Request.Headers.GetValues("Token").First();

            RecursoCurricular.Api.Models.TokenValido tv = new RecursoCurricular.Api.Models.TokenValido();

            RecursoCurricular.Api.Models.Result result = tv.ValidateToken(token);

            if (!result.Status.Equals("OK"))
            {
                return result;
            }

            RecursoCurricular.RecursosCurriculares.Aprendizaje aprendizaje = RecursoCurricular.RecursosCurriculares.Aprendizaje.Get(parametro.AnioNumero, parametro.TipoEducacionCodigo, parametro.GradoCodigo, parametro.SectorId, parametro.Id);

            return new RecursoCurricular.Api.Areas.RecursosCurriculares.Models.AprendizajeEsperado
            {
                Status = "OK",
                Message = "Correcto",
                Item = aprendizaje
            };
        }

        [HttpPost]
        [Route("api/AprendizajesEsperados")]
        public RecursoCurricular.Api.Models.Result AprendizajesEsperados([FromBody] RecursoCurricular.Api.Areas.RecursosCurriculares.Models.Parametro parametro)
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

            List<RecursoCurricular.RecursosCurriculares.Aprendizaje> aprendizajes = RecursoCurricular.RecursosCurriculares.Aprendizaje.GetAll(anio, grado, sector);

            return new RecursoCurricular.Api.Areas.RecursosCurriculares.Models.AprendizajeEsperado { Status = "OK", Message = "Correcto", Lista = aprendizajes };
        }

        [HttpPost]
        [Route("api/AprendizajeUnidad")]
        public RecursoCurricular.Api.Models.Result AprendizajesEsperadosUnidad([FromBody] RecursoCurricular.Api.Areas.RecursosCurriculares.Models.Parametro parametro)
        {
            string token = this.Request.Headers.GetValues("Token").First();

            RecursoCurricular.Api.Models.TokenValido tv = new RecursoCurricular.Api.Models.TokenValido();

            RecursoCurricular.Api.Models.Result result = tv.ValidateToken(token);

            if (!result.Status.Equals("OK"))
            {
                return result;
            }

            RecursoCurricular.Anio anio = RecursoCurricular.Anio.Get(parametro.AnioNumero);

            RecursoCurricular.RecursosCurriculares.Unidad unidad = RecursoCurricular.RecursosCurriculares.Unidad.Get(parametro.TipoEducacionCodigo, parametro.AnioNumero, parametro.GradoCodigo, parametro.SectorId, parametro.UnidadId);

            List<RecursoCurricular.RecursosCurriculares.Aprendizaje> aprendizajes = RecursoCurricular.RecursosCurriculares.Aprendizaje.GetAll(anio, unidad);

            return new RecursoCurricular.Api.Areas.RecursosCurriculares.Models.AprendizajeEsperado { Status = "OK", Message = "Correcto", Lista = aprendizajes };
        }
    }
}