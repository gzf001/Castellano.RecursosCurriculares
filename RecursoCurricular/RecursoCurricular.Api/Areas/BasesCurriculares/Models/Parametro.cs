using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Api.Areas.BasesCurriculares.Models
{
    public class Parametro
    {
        public int AnioNumero
        {
            get;
            set;
        }

        public int TipoEducacionCodigo
        {
            get;
            set;
        }

        public Guid SectorId
        {
            get;
            set;
        }
    }
}