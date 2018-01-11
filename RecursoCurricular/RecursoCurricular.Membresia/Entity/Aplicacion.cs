using System;
using System.Data.Linq;
namespace RecursoCurricular.Membresia
{
	[Serializable]
	public partial class Aplicacion : IEquatable<Aplicacion>
	{
		public Aplicacion()
		{
			this.Id = Guid.NewGuid();
		}

		public Guid Id { get; set; }

		public Nullable<Guid> MenuItemId { get; set; }

		public String Nombre { get; set; }

		public String Clave { get; set; }

		public Byte Orden { get; set; }

		public void Attach()
		{
			Context.Instancia.Aplicaciones.Attach(this);
		}

		public bool Equals(Aplicacion other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.Id.Equals(Id);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(Aplicacion)) return false;
			return Equals((Aplicacion)obj);
		}

		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}
	}
}