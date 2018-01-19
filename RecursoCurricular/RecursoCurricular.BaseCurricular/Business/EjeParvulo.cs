using System;
using System.Collections.Generic;
using System.Linq;
using RecursoCurricular.Educacion;

namespace RecursoCurricular.BaseCurricular
{
    public partial class EjeParvulo
    {
        public static List<EjeParvulo> GetAll()
        {
            return
                (
                from query in Query.GetEjeParvulos()
                select query
                ).ToList<EjeParvulo>();
        }

        public static List<EjeParvulo> GetAll(AmbitoExperienciaAprendizaje ambitoExperienciaAprendizaje, NucleoAprendizaje nucleAprendizaje, Ciclo ciclo)
        {
            return
                (
                from query in Query.GetEjeParvulos(ambitoExperienciaAprendizaje, nucleAprendizaje, ciclo)
                orderby query.Numero
                select query
                ).ToList<EjeParvulo>();
        }
    }
}