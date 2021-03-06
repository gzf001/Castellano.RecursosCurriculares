using System;
using System.Data.Linq;
namespace RecursoCurricular
{
	[Serializable]
	public partial class Sistema : IEquatable<Sistema>
	{
		public Sistema()
		{
			this.Id = Guid.NewGuid();
		}

		public Guid Id { get; set; }

		public String Nombre { get; set; }

		public String Url { get; set; }

		public Boolean Activo { get; set; }

		public void Attach()
		{
			Context.Instancia.Sistemas.Attach(this);
		}

		public bool Equals(Sistema other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.Id.Equals(Id);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(Sistema)) return false;
			return Equals((Sistema)obj);
		}

		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}
	}
}