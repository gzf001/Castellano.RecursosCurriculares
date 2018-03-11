using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RecursoCurricular.Api.Areas.RecursosCurriculares.Controllers
{
    public class ContenidoController : ApiController
    {
        [HttpGet]
        public RecursoCurricular.Api.Models.Result Contenido([FromUri]RecursoCurricular.Api.Areas.RecursosCurriculares.Models.Parametro parametro)
        {
            string token = this.Request.Headers.GetValues("Token").First();

            RecursoCurricular.Api.Models.TokenValido tv = new RecursoCurricular.Api.Models.TokenValido();

            RecursoCurricular.Api.Models.Result result = tv.ValidateToken(token);

            if (!result.Status.Equals("OK"))
            {
                return result;
            }

            RecursoCurricular.RecursosCurriculares.Contenido contenido = RecursoCurricular.RecursosCurriculares.Contenido.Get(parametro.TipoEducacionCodigo, parametro.AnioNumero, parametro.GradoCodigo, parametro.SectorId, parametro.EjeId, parametro.Id);

            return new RecursoCurricular.Api.Areas.RecursosCurriculares.Models.Contenido
            {
                Status = "OK",
                Message = "Correcto",
                Item = contenido
            };
        }

        [HttpPost]
        public RecursoCurricular.Api.Models.Result Contenidos([FromBody] RecursoCurricular.Api.Areas.RecursosCurriculares.Models.Parametro parametro)
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

            RecursoCurricular.RecursosCurriculares.Eje eje = RecursoCurricular.RecursosCurriculares.Eje.Get(parametro.AnioNumero, parametro.SectorId, parametro.EjeId);

            List<RecursoCurricular.RecursosCurriculares.Contenido> contenidos = RecursoCurricular.RecursosCurriculares.Contenido.GetAll(grado, sector, eje);

            return new RecursoCurricular.Api.Areas.RecursosCurriculares.Models.Contenido { Status = "OK", Message = "Correcto", Contenidos = contenidos };
        }
    }
}