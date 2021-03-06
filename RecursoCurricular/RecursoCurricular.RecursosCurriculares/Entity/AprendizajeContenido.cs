using System;
using System.Data.Linq;
using System.Runtime.Serialization;

namespace RecursoCurricular.RecursosCurriculares
{
    [Serializable]
    [DataContract]
    public partial class AprendizajeContenido : IEquatable<AprendizajeContenido>
	{
		public AprendizajeContenido()
		{
			this.anio = default(EntityRef<RecursoCurricular.Anio>);
			this.aprendizaje = default(EntityRef<RecursoCurricular.RecursosCurriculares.Aprendizaje>);
			this.contenido = default(EntityRef<RecursoCurricular.RecursosCurriculares.Contenido>);
			this.grado = default(EntityRef<RecursoCurricular.Educacion.Grado>);
			this.sector = default(EntityRef<RecursoCurricular.Educacion.Sector>);
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
        public Guid AprendizajeId { get; set; }

        [DataMember]
        public Guid EjeId { get; set; }

        [DataMember]
        public Guid ContenidoId { get; set; }

		[NonSerialized]
		private EntityRef<RecursoCurricular.Anio> anio;
		public RecursoCurricular.Anio Anio
		{
			get { return this.anio.Entity; }
			set { this.anio.Entity = value; }
		}

		[NonSerialized]
        [DataMember]
        private EntityRef<RecursoCurricular.RecursosCurriculares.Aprendizaje> aprendizaje;
		public RecursoCurricular.RecursosCurriculares.Aprendizaje Aprendizaje
		{
			get { return this.aprendizaje.Entity; }
			set { this.aprendizaje.Entity = value; }
		}

		[NonSerialized]
        [DataMember]
        private EntityRef<RecursoCurricular.RecursosCurriculares.Contenido> contenido;
		public RecursoCurricular.RecursosCurriculares.Contenido Contenido
		{
			get { return this.contenido.Entity; }
			set { this.contenido.Entity = value; }
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

		public void Attach()
		{
			Context.Instancia.AprendizajeContenidos.Attach(this);
		}

		public bool Equals(AprendizajeContenido other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.AnoNumero.Equals(AnoNumero) && other.TipoEducacionCodigo.Equals(TipoEducacionCodigo) && other.GradoCodigo.Equals(GradoCodigo) && other.SectorId.Equals(SectorId) && other.AprendizajeId.Equals(AprendizajeId) && other.EjeId.Equals(EjeId) && other.ContenidoId.Equals(ContenidoId);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(AprendizajeContenido)) return false;
			return Equals((AprendizajeContenido)obj);
		}

		public override int GetHashCode()
		{
			return this.AnoNumero.GetHashCode() ^ this.TipoEducacionCodigo.GetHashCode() ^ this.GradoCodigo.GetHashCode() ^ this.SectorId.GetHashCode() ^ this.AprendizajeId.GetHashCode() ^ this.EjeId.GetHashCode() ^ this.ContenidoId.GetHashCode();
		}
	}
}