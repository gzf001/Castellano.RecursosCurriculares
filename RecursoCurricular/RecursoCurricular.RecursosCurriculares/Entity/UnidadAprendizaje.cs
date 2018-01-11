using System;
using System.Data.Linq;
namespace RecursoCurricular.RecursosCurriculares
{
	[Serializable]
	public partial class UnidadAprendizaje : IEquatable<UnidadAprendizaje>
	{
		public UnidadAprendizaje()
		{
			this.ano = default(EntityRef<RecursoCurricular.Ano>);
			this.aprendizaje = default(EntityRef<RecursoCurricular.RecursosCurriculares.Aprendizaje>);
			this.unidad = default(EntityRef<RecursoCurricular.RecursosCurriculares.Unidad>);
		}

		public Int32 AnoNumero { get; set; }

		public Int32 TipoEducacionCodigo { get; set; }

		public Int32 GradoCodigo { get; set; }

		public Guid SectorId { get; set; }

		public Guid UnidadId { get; set; }

		public Guid AprendizajeId { get; set; }

		[NonSerialized]
		private EntityRef<RecursoCurricular.Ano> ano;
		public RecursoCurricular.Ano Ano
		{
			get { return this.ano.Entity; }
			set { this.ano.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<RecursoCurricular.RecursosCurriculares.Aprendizaje> aprendizaje;
		public RecursoCurricular.RecursosCurriculares.Aprendizaje Aprendizaje
		{
			get { return this.aprendizaje.Entity; }
			set { this.aprendizaje.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<RecursoCurricular.RecursosCurriculares.Unidad> unidad;
		public RecursoCurricular.RecursosCurriculares.Unidad Unidad
		{
			get { return this.unidad.Entity; }
			set { this.unidad.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.UnidadAprendizajes.Attach(this);
		}

		public bool Equals(UnidadAprendizaje other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.AnoNumero.Equals(AnoNumero) && other.TipoEducacionCodigo.Equals(TipoEducacionCodigo) && other.GradoCodigo.Equals(GradoCodigo) && other.SectorId.Equals(SectorId) && other.UnidadId.Equals(UnidadId) && other.AprendizajeId.Equals(AprendizajeId);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(UnidadAprendizaje)) return false;
			return Equals((UnidadAprendizaje)obj);
		}

		public override int GetHashCode()
		{
			return this.AnoNumero.GetHashCode() ^ this.TipoEducacionCodigo.GetHashCode() ^ this.GradoCodigo.GetHashCode() ^ this.SectorId.GetHashCode() ^ this.UnidadId.GetHashCode() ^ this.AprendizajeId.GetHashCode();
		}
	}
}