using System;
using System.Linq;
namespace RecursoCurricular.Educacion
{
	internal static class Query
	{
		#region Ciclo
		internal static IQueryable<Ciclo> GetCiclos()
		{
			return
				from ciclo in Context.Instancia.Ciclos
				select ciclo;
		}
		#endregion

		#region Grado
		internal static IQueryable<Grado> GetGrados()
		{
			return
				from grado in Context.Instancia.Grados
				select grado;
		}

        internal static IQueryable<Grado> GetGrados(RecursoCurricular.Educacion.TipoEducacion tipoEducacion)
        {
            return
                from grado in GetGrados()
                where grado.TipoEducacion == tipoEducacion
                select grado;
        }
        #endregion

        #region Sector
        internal static IQueryable<Sector> GetSectores()
		{
			return
				from sector in Context.Instancia.Sectores
				select sector;
		}
		#endregion

		#region TipoEducacion
		internal static IQueryable<TipoEducacion> GetTipoEducaciones()
		{
			return
				from tipoEducacion in Context.Instancia.TipoEducaciones
				select tipoEducacion;
		}
		#endregion
	}
}