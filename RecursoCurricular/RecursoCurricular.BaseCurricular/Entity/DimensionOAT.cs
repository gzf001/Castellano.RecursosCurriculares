using System;
using System.Data.Linq;
namespace RecursoCurricular.BaseCurricular
{
	[Serializable]
	public partial class DimensionOAT : IEquatable<DimensionOAT>
	{
		public DimensionOAT()
		{
			this.Id = Guid.NewGuid();
			this.ano = default(EntityRef<RecursoCurricular.Ano>);
		}

		public Guid Id { get; set; }

		public Int32 AnoNumero { get; set; }

		public Int32 Numero { get; set; }

		public String Nombre { get; set; }

		public String Descripcion { get; set; }

		[NonSerialized]
		private EntityRef<RecursoCurricular.Ano> ano;
		public RecursoCurricular.Ano Ano
		{
			get { return this.ano.Entity; }
			set { this.ano.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.DimensionOATes.Attach(this);
		}

		public bool Equals(DimensionOAT other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.Id.Equals(Id) && other.AnoNumero.Equals(AnoNumero);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(DimensionOAT)) return false;
			return Equals((DimensionOAT)obj);
		}

		public override int GetHashCode()
		{
			return this.Id.GetHashCode() ^ this.AnoNumero.GetHashCode();
		}
	}
}