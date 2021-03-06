using System;
using System.Data.Linq;
namespace RecursoCurricular
{
	[Serializable]
	public partial class Nacionalidad : IEquatable<Nacionalidad>
	{
		public Nacionalidad()
		{
		}

		public Int16 Codigo { get; set; }

		public String Nombre { get; set; }

		public Boolean Extranjero { get; set; }

		public void Attach()
		{
			Context.Instancia.Nacionalidades.Attach(this);
		}

		public bool Equals(Nacionalidad other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.Codigo.Equals(Codigo);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(Nacionalidad)) return false;
			return Equals((Nacionalidad)obj);
		}

		public override int GetHashCode()
		{
			return this.Codigo.GetHashCode();
		}
	}
}