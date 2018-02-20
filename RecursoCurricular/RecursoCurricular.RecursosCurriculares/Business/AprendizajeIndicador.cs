using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.RecursosCurriculares
{
    public partial class AprendizajeIndicador
    {
        public static List<AprendizajeIndicador> GetAll()
        {
            return
                (
                from query in Query.GetAprendizajeIndicadores()
                select query
                ).ToList<AprendizajeIndicador>();
        }

        public static List<AprendizajeIndicador> GetAll(RecursoCurricular.RecursosCurriculares.Aprendizaje aprendizaje)
        {
            return
                (
                from query in Query.GetAprendizajeIndicadores(aprendizaje)
                orderby query.Numero
                select query
                ).ToList<AprendizajeIndicador>();
        }
    }
}