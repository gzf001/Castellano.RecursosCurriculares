using System;
using System.Data.Linq;
namespace RecursoCurricular
{
	[Serializable]
	public partial class NivelEducacional : IEquatable<NivelEducacional>
	{
		public NivelEducacional()
		{
		}

		public Int32 Codigo { get; set; }

		public String Nombre { get; set; }

		public void Attach()
		{
			Context.Instancia.NivelEducacionales.Attach(this);
		}

		public bool Equals(NivelEducacional other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.Codigo.Equals(Codigo);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(NivelEducacional)) return false;
			return Equals((NivelEducacional)obj);
		}

		public override int GetHashCode()
		{
			return this.Codigo.GetHashCode();
		}
	}
}