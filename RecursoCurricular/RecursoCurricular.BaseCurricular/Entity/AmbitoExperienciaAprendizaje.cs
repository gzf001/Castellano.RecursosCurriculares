using System;
using System.Data.Linq;
namespace RecursoCurricular.BaseCurricular
{
	[Serializable]
	public partial class AmbitoExperienciaAprendizaje : IEquatable<AmbitoExperienciaAprendizaje>
	{
		public AmbitoExperienciaAprendizaje()
		{
			this.ano = default(EntityRef<RecursoCurricular.Ano>);
		}

		public Int32 AnoNumero { get; set; }

		public Int32 Codigo { get; set; }

		public String Nombre { get; set; }

		[NonSerialized]
		private EntityRef<RecursoCurricular.Ano> ano;
		public RecursoCurricular.Ano Ano
		{
			get { return this.ano.Entity; }
			set { this.ano.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.AmbitoExperienciaAprendizajes.Attach(this);
		}

		public bool Equals(AmbitoExperienciaAprendizaje other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.AnoNumero.Equals(AnoNumero) && other.Codigo.Equals(Codigo);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(AmbitoExperienciaAprendizaje)) return false;
			return Equals((AmbitoExperienciaAprendizaje)obj);
		}

		public override int GetHashCode()
		{
			return this.AnoNumero.GetHashCode() ^ this.Codigo.GetHashCode();
		}
	}
}