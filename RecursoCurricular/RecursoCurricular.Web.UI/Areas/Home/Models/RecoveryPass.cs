﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Web.UI.Areas.Home.Models
{
    public class RecoveryPass
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar el R.U.N.")]
        public string RUN
        {
            get;
            set;
        }
    }
}