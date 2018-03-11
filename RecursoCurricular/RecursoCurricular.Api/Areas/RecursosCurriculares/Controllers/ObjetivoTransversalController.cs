using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RecursoCurricular.Api.Areas.RecursosCurriculares.Controllers
{
    public class ObjetivoTransversalController : ApiController
    {
        [HttpGet]
        public RecursoCurricular.Api.Models.Result ObjetivoTransversal([FromUri]RecursoCurricular.Api.Areas.RecursosCurriculares.Models.Parametro parametro)
        {
            string token = this.Request.Headers.GetValues("Token").First();

            RecursoCurricular.Api.Models.TokenValido tv = new RecursoCurricular.Api.Models.TokenValido();

            RecursoCurricular.Api.Models.Result result = tv.ValidateToken(token);

            if (!result.Status.Equals("OK"))
            {
                return result;
            }

            RecursoCurricular.RecursosCurriculares.ObjetivoTransversal objetivoTransversal = RecursoCurricular.RecursosCurriculares.ObjetivoTransversal.Get(parametro.AnioNumero, parametro.TipoEducacionCodigo, parametro.GradoCodigo, parametro.SectorId, parametro.UnidadId, parametro.Id);

            return new RecursoCurricular.Api.Areas.RecursosCurriculares.Models.ObjetivoTransversal
            {
                Status = "OK",
                Message = "Correcto",
                Item = objetivoTransversal
            };
        }

        [HttpPost]
        public RecursoCurricular.Api.Models.Result ObjetivosTransversales([FromBody] RecursoCurricular.Api.Areas.RecursosCurriculares.Models.Parametro parametro)
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

            RecursoCurricular.RecursosCurriculares.Unidad unidad = RecursoCurricular.RecursosCurriculares.Unidad.Get(parametro.TipoEducacionCodigo, parametro.AnioNumero, parametro.GradoCodigo, parametro.SectorId, parametro.UnidadId);

            List<RecursoCurricular.RecursosCurriculares.ObjetivoTransversal> objetivosTransversales = RecursoCurricular.RecursosCurriculares.ObjetivoTransversal.GetAll(unidad);

            return new RecursoCurricular.Api.Areas.RecursosCurriculares.Models.ObjetivoTransversal { Status = "OK", Message = "Correcto", ObjetivosTransversales = objetivosTransversales };
        }
    }
}