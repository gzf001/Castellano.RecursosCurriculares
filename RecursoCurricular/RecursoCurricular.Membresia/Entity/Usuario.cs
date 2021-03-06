using System;
using System.Data.Linq;
namespace RecursoCurricular.Membresia
{
	[Serializable]
	public partial class Usuario : IEquatable<Usuario>
	{
		public Usuario()
		{
			this.persona = default(EntityRef<RecursoCurricular.Persona>);
		}

		public Guid Id { get; set; }

		public String Password { get; set; }

		public Boolean Aprobado { get; set; }

		public Boolean Bloqueado { get; set; }

		public DateTime Creacion { get; set; }

		public DateTime UltimaActividad { get; set; }

		public DateTime UltimoAcceso { get; set; }

		public Nullable<DateTime> UltimoCambioPassword { get; set; }

		public Nullable<DateTime> UltimoDesbloqueo { get; set; }

		public Int16 NumeroIntentosFallidos { get; set; }

		public Nullable<DateTime> FechaIntentoFallido { get; set; }

		[NonSerialized]
		private EntityRef<RecursoCurricular.Persona> persona;
		public RecursoCurricular.Persona Persona
		{
			get { return this.persona.Entity; }
			set { this.persona.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.Usuarios.Attach(this);
		}

		public bool Equals(Usuario other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.Id.Equals(Id);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(Usuario)) return false;
			return Equals((Usuario)obj);
		}

		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}
	}
}