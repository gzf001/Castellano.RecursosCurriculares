﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Api.Areas.BasesCurriculares.Models
{
    public class Parametro : RecursoCurricular.Api.Models.Parametro
    {
        public Guid EjeId
        {
            get;
            set;
        }

        public Guid ObjetivoAprendizajeId
        {
            get;
            set;
        }

        public Guid HabilidadId
        {
            get;
            set;
        }

        public Guid Id
        {
            get;
            set;
        }
    }
}