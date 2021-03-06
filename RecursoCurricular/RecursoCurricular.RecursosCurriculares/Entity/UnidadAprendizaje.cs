using System;
using System.Data.Linq;
using System.Runtime.Serialization;

namespace RecursoCurricular.RecursosCurriculares
{
    [Serializable]
    [DataContract]
    public partial class UnidadAprendizaje : IEquatable<UnidadAprendizaje>
	{
		public UnidadAprendizaje()
		{
			this.anio = default(EntityRef<RecursoCurricular.Anio>);
			this.aprendizaje = default(EntityRef<RecursoCurricular.RecursosCurriculares.Aprendizaje>);
			this.grado = default(EntityRef<RecursoCurricular.Educacion.Grado>);
			this.sector = default(EntityRef<RecursoCurricular.Educacion.Sector>);
			this.unidad = default(EntityRef<RecursoCurricular.RecursosCurriculares.Unidad>);
		}

        [DataMember]
        public Int32 AnoNumero { get; set; }

        [DataMember]
        public Int32 TipoEducacionCodigo { get; set; }

        [DataMember]
        public Int32 GradoCodigo { get; set; }

        [DataMember]
        public Guid SectorId { get; set; }

        [DataMember]
        public Guid UnidadId { get; set; }

        [DataMember]
        public Guid AprendizajeId { get; set; }

		[NonSerialized]
		private EntityRef<RecursoCurricular.Anio> anio;
		public RecursoCurricular.Anio Anio
		{
			get { return this.anio.Entity; }
			set { this.anio.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<RecursoCurricular.RecursosCurriculares.Aprendizaje> aprendizaje;
		public RecursoCurricular.RecursosCurriculares.Aprendizaje Aprendizaje
		{
			get { return this.aprendizaje.Entity; }
			set { this.aprendizaje.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<RecursoCurricular.Educacion.Grado> grado;
		public RecursoCurricular.Educacion.Grado Grado
		{
			get { return this.grado.Entity; }
			set { this.grado.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<RecursoCurricular.Educacion.Sector> sector;
		public RecursoCurricular.Educacion.Sector Sector
		{
			get { return this.sector.Entity; }
			set { this.sector.Entity = value; }
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