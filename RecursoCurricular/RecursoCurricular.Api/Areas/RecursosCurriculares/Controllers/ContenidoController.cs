using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RecursoCurricular.Api.Areas.RecursosCurriculares.Controllers
{
    /// <summary>
    /// Controlador de contenidos
    /// </summary>
    public class ContenidoController : ApiController
    {
        /// <summary>
        /// Retorna un contenido especifico
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Contenido")]
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

        /// <summary>
        /// Retorna todos los contenidos de un eje, un grado y un sector
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Contenidos")]
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

            return new RecursoCurricular.Api.Areas.RecursosCurriculares.Models.Contenido { Status = "OK", Message = "Correcto", Lista = contenidos };
        }

        /// <summary>
        /// Retorna todos los contenidos asociados a un aprendizaje esperado
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/AprendizajeContenido")]
        public RecursoCurricular.Api.Models.Result AprendizajeContenido([FromBody]RecursoCurricular.Api.Areas.RecursosCurriculares.Models.Parametro parametro)
        {
            string token = this.Request.Headers.GetValues("Token").First();

            RecursoCurricular.Api.Models.TokenValido tv = new RecursoCurricular.Api.Models.TokenValido();

            RecursoCurricular.Api.Models.Result result = tv.ValidateToken(token);

            if (!result.Status.Equals("OK"))
            {
                return result;
            }

            RecursoCurricular.RecursosCurriculares.Aprendizaje aprendizaje = RecursoCurricular.RecursosCurriculares.Aprendizaje.Get(parametro.AnioNumero, parametro.TipoEducacionCodigo, parametro.GradoCodigo, parametro.SectorId, parametro.AprendizajeId);

            List<RecursoCurricular.RecursosCurriculares.Contenido> contenidos = RecursoCurricular.RecursosCurriculares.Contenido.GetAll(aprendizaje);

            return new RecursoCurricular.Api.Areas.RecursosCurriculares.Models.Contenido { Status = "OK", Message = "Correcto", Lista = contenidos };
        }

        [HttpPost]
        [Route("api/ContenidosGradoSector")]
        public RecursoCurricular.Api.Models.Result ContenidosGradoSector([FromBody] RecursoCurricular.Api.Areas.RecursosCurriculares.Models.Parametro parametro)
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

            List<RecursoCurricular.RecursosCurriculares.Contenido> contenidos = RecursoCurricular.RecursosCurriculares.Contenido.GetAll(anio, sector, grado);

            return new RecursoCurricular.Api.Areas.RecursosCurriculares.Models.Contenido { Status = "OK", Message = "Correcto", Lista = contenidos };
        }
    }
}