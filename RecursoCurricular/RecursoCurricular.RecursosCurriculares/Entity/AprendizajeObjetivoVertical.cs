using System;
using System.Data.Linq;
namespace RecursoCurricular.RecursosCurriculares
{
	[Serializable]
	public partial class AprendizajeObjetivoVertical : IEquatable<AprendizajeObjetivoVertical>
	{
		public AprendizajeObjetivoVertical()
		{
			this.ano = default(EntityRef<RecursoCurricular.Ano>);
			this.objetivoVertical = default(EntityRef<RecursoCurricular.RecursosCurriculares.ObjetivoVertical>);
		}

		public Int32 AnoNumero { get; set; }

		public Int32 TipoEducacionCodigo { get; set; }

		public Int32 GradoCodigo { get; set; }

		public Guid SectorId { get; set; }

		public Guid AprendizajeId { get; set; }

		public Guid ObjetivoVerticalId { get; set; }

		[NonSerialized]
		private EntityRef<RecursoCurricular.Ano> ano;
		public RecursoCurricular.Ano Ano
		{
			get { return this.ano.Entity; }
			set { this.ano.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<RecursoCurricular.RecursosCurriculares.ObjetivoVertical> objetivoVertical;
		public RecursoCurricular.RecursosCurriculares.ObjetivoVertical ObjetivoVertical
		{
			get { return this.objetivoVertical.Entity; }
			set { this.objetivoVertical.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.AprendizajeObjetivoVerticales.Attach(this);
		}

		public bool Equals(AprendizajeObjetivoVertical other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.AnoNumero.Equals(AnoNumero) && other.TipoEducacionCodigo.Equals(TipoEducacionCodigo) && other.GradoCodigo.Equals(GradoCodigo) && other.SectorId.Equals(SectorId) && other.AprendizajeId.Equals(AprendizajeId) && other.ObjetivoVerticalId.Equals(ObjetivoVerticalId);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(AprendizajeObjetivoVertical)) return false;
			return Equals((AprendizajeObjetivoVertical)obj);
		}

		public override int GetHashCode()
		{
			return this.AnoNumero.GetHashCode() ^ this.TipoEducacionCodigo.GetHashCode() ^ this.GradoCodigo.GetHashCode() ^ this.SectorId.GetHashCode() ^ this.AprendizajeId.GetHashCode() ^ this.ObjetivoVerticalId.GetHashCode();
		}
	}
}