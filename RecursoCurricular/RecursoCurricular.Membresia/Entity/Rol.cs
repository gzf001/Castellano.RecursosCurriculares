using System;
using System.Data.Linq;
namespace RecursoCurricular.Membresia
{
	[Serializable]
	public partial class Rol : IEquatable<Rol>
	{
		public Rol()
		{
			this.Id = Guid.NewGuid();
		}

		public Guid Id { get; set; }

		public String Nombre { get; set; }

		public String Clave { get; set; }

		public void Attach()
		{
			Context.Instancia.Roles.Attach(this);
		}

		public bool Equals(Rol other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.Id.Equals(Id);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(Rol)) return false;
			return Equals((Rol)obj);
		}

		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}
	}
}