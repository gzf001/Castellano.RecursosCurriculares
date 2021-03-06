using System;
using System.Data.Linq;
namespace RecursoCurricular
{
	[Serializable]
	public partial class Region : IEquatable<Region>
	{
		public Region()
		{
		}

		public Int16 Codigo { get; set; }

		public String Nombre { get; set; }

		public void Attach()
		{
			Context.Instancia.Regiones.Attach(this);
		}

		public bool Equals(Region other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.Codigo.Equals(Codigo);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(Region)) return false;
			return Equals((Region)obj);
		}

		public override int GetHashCode()
		{
			return this.Codigo.GetHashCode();
		}
	}
}