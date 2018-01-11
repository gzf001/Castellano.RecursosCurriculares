using System;
using System.Data.Linq;
namespace RecursoCurricular.BaseCurricular
{
	[Serializable]
	public partial class Eje : IEquatable<Eje>
	{
		public Eje()
		{
			this.Id = Guid.NewGuid();
			this.ano = default(EntityRef<RecursoCurricular.Ano>);
			this.sector = default(EntityRef<RecursoCurricular.Educacion.Sector>);
		}

		public Int32 AnoNumero { get; set; }

		public Guid SectorId { get; set; }

		public Guid Id { get; set; }

		public Int32 Numero { get; set; }

		public String Nombre { get; set; }

		[NonSerialized]
		private EntityRef<RecursoCurricular.Ano> ano;
		public RecursoCurricular.Ano Ano
		{
			get { return this.ano.Entity; }
			set { this.ano.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<RecursoCurricular.Educacion.Sector> sector;
		public RecursoCurricular.Educacion.Sector Sector
		{
			get { return this.sector.Entity; }
			set { this.sector.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.Ejes.Attach(this);
		}

		public bool Equals(Eje other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.AnoNumero.Equals(AnoNumero) && other.SectorId.Equals(SectorId) && other.Id.Equals(Id);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(Eje)) return false;
			return Equals((Eje)obj);
		}

		public override int GetHashCode()
		{
			return this.AnoNumero.GetHashCode() ^ this.SectorId.GetHashCode() ^ this.Id.GetHashCode();
		}
	}
}