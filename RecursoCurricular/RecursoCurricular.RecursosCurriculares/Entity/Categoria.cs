using System;
using System.Data.Linq;
namespace RecursoCurricular.RecursosCurriculares
{
	[Serializable]
	public partial class Categoria : IEquatable<Categoria>
	{
		public Categoria()
		{
		}

		public Int32 Codigo { get; set; }

		public String Nombre { get; set; }

		public void Attach()
		{
			Context.Instancia.Categorias.Attach(this);
		}

		public bool Equals(Categoria other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.Codigo.Equals(Codigo);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(Categoria)) return false;
			return Equals((Categoria)obj);
		}

		public override int GetHashCode()
		{
			return this.Codigo.GetHashCode();
		}
	}
}