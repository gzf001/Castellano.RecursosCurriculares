﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Api.Areas.BasesCurriculares.Models
{
    public class Actitud : RecursoCurricular.Api.Models.Result
    {
        public RecursoCurricular.BaseCurricular.Actitud Item
        {
            get;
            set;
        }

        public List<RecursoCurricular.BaseCurricular.Actitud> Actitudes
        {
            get;
            set;
        }
    }
}