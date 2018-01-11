using System;
using System.Data.Linq;
namespace RecursoCurricular.BaseCurricular
{
	[Serializable]
	public partial class ObjetivoAprendizajeTransversal : IEquatable<ObjetivoAprendizajeTransversal>
	{
		public ObjetivoAprendizajeTransversal()
		{
			this.Id = Guid.NewGuid();
			this.ano = default(EntityRef<RecursoCurricular.Ano>);
			this.dimensionOAT = default(EntityRef<RecursoCurricular.BaseCurricular.DimensionOAT>);
		}

		public Guid DimensionOATId { get; set; }

		public Int32 AnoNumero { get; set; }

		public Guid Id { get; set; }

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

		[NonSerialized]
		private EntityRef<RecursoCurricular.BaseCurricular.DimensionOAT> dimensionOAT;
		public RecursoCurricular.BaseCurricular.DimensionOAT DimensionOAT
		{
			get { return this.dimensionOAT.Entity; }
			set { this.dimensionOAT.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.ObjetivoAprendizajeTransversales.Attach(this);
		}

		public bool Equals(ObjetivoAprendizajeTransversal other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.DimensionOATId.Equals(DimensionOATId) && other.AnoNumero.Equals(AnoNumero) && other.Id.Equals(Id);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(ObjetivoAprendizajeTransversal)) return false;
			return Equals((ObjetivoAprendizajeTransversal)obj);
		}

		public override int GetHashCode()
		{
			return this.DimensionOATId.GetHashCode() ^ this.AnoNumero.GetHashCode() ^ this.Id.GetHashCode();
		}
	}
}