using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RecursoCurricular.Api.Areas.BasesCurriculares.Controllers
{
    public class ActitudController : ApiController
    {
        [HttpPost]
        public RecursoCurricular.Api.Areas.BasesCurriculares.Models.Actitud Actitudes([FromBody] RecursoCurricular.Api.Areas.BasesCurriculares.Models.Parametro parametro)
        {
            string token = this.Request.Headers.GetValues("Token").First();

            if (string.IsNullOrEmpty(token))
            {
                return new RecursoCurricular.Api.Areas.BasesCurriculares.Models.Actitud { Status = "Error", Message = "Sin token" };
            }

            if (!RecursoCurricular.Helper.ValidateToken(token))
            {
                return new RecursoCurricular.Api.Areas.BasesCurriculares.Models.Actitud { Status = "Error", Message = "Token inválido" };
            }

            RecursoCurricular.Anio anio = RecursoCurricular.Anio.Get(parametro.AnioNumero);

            RecursoCurricular.Educacion.TipoEducacion tipoEducacion = RecursoCurricular.Educacion.TipoEducacion.Get(parametro.TipoEducacionCodigo);

            RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(parametro.SectorId);

            List<RecursoCurricular.BaseCurricular.Actitud> actitudes = RecursoCurricular.BaseCurricular.Actitud.GetAll(tipoEducacion, anio, sector);

            return new RecursoCurricular.Api.Areas.BasesCurriculares.Models.Actitud { Status = "OK", Message = "Correcto", Actitudes = actitudes };
        }
    }
}