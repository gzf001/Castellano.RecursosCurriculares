using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular
{
	public partial class Sistema
	{
		private static List<Sistema> GetAll()
		{
			return
				(
				from query in Query.GetSistemas()
				select query
				).ToList<Sistema>();
		}

        public static List<Sistema> GetAll(bool activo)
        {
            return activo ? Sistema.GetAll().Where<RecursoCurricular.Sistema>(x => x.Activo).ToList<RecursoCurricular.Sistema>() : Sistema.GetAll();
        }
    }
}