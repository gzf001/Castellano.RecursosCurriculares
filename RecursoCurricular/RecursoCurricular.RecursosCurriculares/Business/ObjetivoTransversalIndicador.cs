using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.RecursosCurriculares
{
    public partial class ObjetivoTransversalIndicador
    {
        public static int Last(RecursoCurricular.RecursosCurriculares.ObjetivoTransversal objetivoTransversal)
        {
            IQueryable<ObjetivoTransversalIndicador> objetivoTransversalIndicadores = Query.GetObjetivoTransversalIndicadores(objetivoTransversal);

            return objetivoTransversalIndicadores.Count<ObjetivoTransversalIndicador>() > 0 ? objetivoTransversalIndicadores.Count<ObjetivoTransversalIndicador>() + 1 : 1;
        }

        public static List<ObjetivoTransversalIndicador> GetAll()
        {
            return
                (
                from query in Query.GetObjetivoTransversalIndicadores()
                select query
                ).ToList<ObjetivoTransversalIndicador>();
        }

        public static List<ObjetivoTransversalIndicador> GetAll(RecursoCurricular.RecursosCurriculares.ObjetivoTransversal objetivoTransversal)
        {
            return
                (
                from query in Query.GetObjetivoTransversalIndicadores(objetivoTransversal)
                orderby query.Numero
                select query
                ).ToList<ObjetivoTransversalIndicador>();
        }
    }
}