﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RecursoCurricular.Api.Areas.BasesCurriculares.Controllers
{
    public class ActitudController : ApiController
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

            RecursoCurricular.BaseCurricular.Actitud actitud = RecursoCurricular.BaseCurricular.Actitud.Get(parametro.TipoEducacionCodigo, parametro.AnioNumero, parametro.SectorId, parametro.Id);

            return new RecursoCurricular.Api.Areas.BasesCurriculares.Models.Actitud
            {
                Status = "OK",
                Message = "Correcto",
                Item = actitud
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

            List<RecursoCurricular.BaseCurricular.Actitud> actitudes = RecursoCurricular.BaseCurricular.Actitud.GetAll(tipoEducacion, anio, sector);

            return new RecursoCurricular.Api.Areas.BasesCurriculares.Models.Actitud { Status = "OK", Message = "Correcto", Actitudes = actitudes };
        }
    }
}