using System;
using System.Data.Linq;
namespace RecursoCurricular.RecursosCurriculares
{
	[Serializable]
	public partial class ObjetivoTransversalIndicador : IEquatable<ObjetivoTransversalIndicador>
	{
		public ObjetivoTransversalIndicador()
		{
			this.Id = Guid.NewGuid();
			this.anio = default(EntityRef<RecursoCurricular.Anio>);
			this.objetivoTransversal = default(EntityRef<RecursoCurricular.RecursosCurriculares.ObjetivoTransversal>);
		}

		public Int32 AnoNumero { get; set; }

		public Int32 TipoEducacionCodigo { get; set; }

		public Int32 GradoCodigo { get; set; }

		public Guid SectorId { get; set; }

		public Guid UnidadId { get; set; }

		public Guid ObjetivoTransversalId { get; set; }

		public Guid Id { get; set; }

		public Int32 Numero { get; set; }

		public String Descripcion { get; set; }

		[NonSerialized]
		private EntityRef<RecursoCurricular.Anio> anio;
		public RecursoCurricular.Anio Anio
		{
			get { return this.anio.Entity; }
			set { this.anio.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<RecursoCurricular.RecursosCurriculares.ObjetivoTransversal> objetivoTransversal;
		public RecursoCurricular.RecursosCurriculares.ObjetivoTransversal ObjetivoTransversal
		{
			get { return this.objetivoTransversal.Entity; }
			set { this.objetivoTransversal.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.ObjetivoTransversalIndicadores.Attach(this);
		}

		public bool Equals(ObjetivoTransversalIndicador other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.AnoNumero.Equals(AnoNumero) && other.TipoEducacionCodigo.Equals(TipoEducacionCodigo) && other.GradoCodigo.Equals(GradoCodigo) && other.SectorId.Equals(SectorId) && other.UnidadId.Equals(UnidadId) && other.ObjetivoTransversalId.Equals(ObjetivoTransversalId) && other.Id.Equals(Id);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(ObjetivoTransversalIndicador)) return false;
			return Equals((ObjetivoTransversalIndicador)obj);
		}

		public override int GetHashCode()
		{
			return this.AnoNumero.GetHashCode() ^ this.TipoEducacionCodigo.GetHashCode() ^ this.GradoCodigo.GetHashCode() ^ this.SectorId.GetHashCode() ^ this.UnidadId.GetHashCode() ^ this.ObjetivoTransversalId.GetHashCode() ^ this.Id.GetHashCode();
		}
	}
}