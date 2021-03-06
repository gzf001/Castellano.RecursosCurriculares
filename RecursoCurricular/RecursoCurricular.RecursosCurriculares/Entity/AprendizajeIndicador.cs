using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
using System.Runtime.Serialization;

namespace RecursoCurricular.RecursosCurriculares
{
    [Serializable]
    [DataContract]
    public partial class AprendizajeIndicador : IEquatable<AprendizajeIndicador>
	{
		public AprendizajeIndicador()
		{
			this.Id = Guid.NewGuid();
			this.anio = default(EntityRef<RecursoCurricular.Anio>);
			this.aprendizaje = default(EntityRef<RecursoCurricular.RecursosCurriculares.Aprendizaje>);
			this.categoria = default(EntityRef<RecursoCurricular.RecursosCurriculares.Categoria>);
			this.grado = default(EntityRef<RecursoCurricular.Educacion.Grado>);
			this.sector = default(EntityRef<RecursoCurricular.Educacion.Sector>);
			this.tipoEducacion = default(EntityRef<RecursoCurricular.Educacion.TipoEducacion>);
		}

        [DataMember]
        public Int32 AnoNumero { get; set; }

        [Display(Name = "Tipo de educación:")]
        [Required(ErrorMessage = "El tipo de educación es requerido")]
        [DataMember]
        public Int32 TipoEducacionCodigo { get; set; }

        [Display(Name = "Grado:")]
        [Required(ErrorMessage = "El grado es requerido")]
        [DataMember]
        public Int32 GradoCodigo { get; set; }

        [Display(Name = "Sector:")]
        [Required(ErrorMessage = "El sector es requerido")]
        [DataMember]
        public Guid SectorId { get; set; }

        [Display(Name = "Aprendizaje:")]
        [Required(ErrorMessage = "El aprendizaje es requerido")]
        [DataMember]
        public Guid AprendizajeId { get; set; }

        [Required(ErrorMessage = "El id es requerido")]
        [DataMember]
        public Guid Id { get; set; }

        [Display(Name = "Habilidad Taxonómica:")]
        [DataMember]
        public Nullable<Int32> CategoriaCodigo { get; set; }

        [Display(Name = "Número:")]
        [Required(ErrorMessage = "El número es requerido")]
        [DataMember]
        public Int32 Numero { get; set; }

        [Display(Name = "Indicador:")]
        [Required(ErrorMessage = "La descripción del indicador es requerida")]
        [DataMember]
        public String Descripcion { get; set; }

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
		private EntityRef<RecursoCurricular.RecursosCurriculares.Categoria> categoria;
		public RecursoCurricular.RecursosCurriculares.Categoria Categoria
		{
			get { return this.categoria.Entity; }
			set { this.categoria.Entity = value; }
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
		private EntityRef<RecursoCurricular.Educacion.TipoEducacion> tipoEducacion;
		public RecursoCurricular.Educacion.TipoEducacion TipoEducacion
		{
			get { return this.tipoEducacion.Entity; }
			set { this.tipoEducacion.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.AprendizajeIndicadores.Attach(this);
		}

		public bool Equals(AprendizajeIndicador other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.AnoNumero.Equals(AnoNumero) && other.TipoEducacionCodigo.Equals(TipoEducacionCodigo) && other.GradoCodigo.Equals(GradoCodigo) && other.SectorId.Equals(SectorId) && other.AprendizajeId.Equals(AprendizajeId) && other.Id.Equals(Id);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(AprendizajeIndicador)) return false;
			return Equals((AprendizajeIndicador)obj);
		}

		public override int GetHashCode()
		{
			return this.AnoNumero.GetHashCode() ^ this.TipoEducacionCodigo.GetHashCode() ^ this.GradoCodigo.GetHashCode() ^ this.SectorId.GetHashCode() ^ this.AprendizajeId.GetHashCode() ^ this.Id.GetHashCode();
		}
	}
}