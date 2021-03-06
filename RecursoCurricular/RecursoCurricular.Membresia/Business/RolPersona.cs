using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.Membresia
{
	public partial class RolPersona
	{
		public static bool Exists(Persona persona)
		{
			return Query.GetRolPersonas(persona).Any<RolPersona>();
		}

        public static bool Exists(Persona persona, Rol rol)
        {
            return Query.GetRolPersonas(persona).Any<RolPersona>(x => x.Rol.Equals(rol));
        }

        public static RolPersona Get(Guid rolId, Guid personaId)
		{
			return Query.GetRolPersonas().SingleOrDefault<RolPersona>(x => x.RolId == rolId && x.PersonaId == personaId);
		}

		public static RolPersona Get(Rol rol, Persona persona)
		{
			return Query.GetRolPersonas().SingleOrDefault<RolPersona>(x => x.Rol == rol && x.Persona == persona);
		}

		public static List<RolPersona> GetAll()
		{
			return
				(
				from query in Query.GetRolPersonas()
				select query
				).ToList<RolPersona>();
		}

        public static List<RolPersona> GetAll(Persona persona)
        {
            return
                (
                from query in Query.GetRolPersonas(persona)
                select query
                ).ToList<RolPersona>();
        }
    }
}