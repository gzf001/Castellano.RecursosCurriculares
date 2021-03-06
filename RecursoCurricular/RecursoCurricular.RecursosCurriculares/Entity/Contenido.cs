using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
using System.Runtime.Serialization;

namespace RecursoCurricular.RecursosCurriculares
{
    [Serializable]
    [DataContract]
    public partial class Contenido : IEquatable<Contenido>
	{
		public Contenido()
		{
			this.Id = Guid.NewGuid();
			this.anio = default(EntityRef<RecursoCurricular.Anio>);
			this.eje = default(EntityRef<RecursoCurricular.RecursosCurriculares.Eje>);
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

        [Display(Name = "Sector:")]
        [Required(ErrorMessage = "El sector es requerido")]
        [DataMember]
        public Guid SectorId { get; set; }

        [Display(Name = "Eje:")]
        [Required(ErrorMessage = "El eje es requerido")]
        [DataMember]
        public Guid EjeId { get; set; }

        [Display(Name = "Grado:")]
        [Required(ErrorMessage = "El grado es requerido")]
        [DataMember]
        public Int32 GradoCodigo { get; set; }

        [Required(ErrorMessage = "El id es requerido")]
        [DataMember]
        public Guid Id { get; set; }

        [Display(Name = "Número:")]
        [Required(ErrorMessage = "El número es requerido")]
        [DataMember]
        public Int32 Numero { get; set; }

        [Display(Name = "Contenido:")]
        [Required(ErrorMessage = "La descripción del contenido es requerida")]
        [DataMember]
        public String Descripcion { get; set; }

        [DataMember]
        public Boolean Transversal { get; set; }

		[NonSerialized]
		private EntityRef<RecursoCurricular.Anio> anio;
		public RecursoCurricular.Anio Anio
		{
			get { return this.anio.Entity; }
			set { this.anio.Entity = value; }
		}

        [DataMember]
        private EntityRef<RecursoCurricular.RecursosCurriculares.Eje> eje;
		public RecursoCurricular.RecursosCurriculares.Eje Eje
		{
			get { return this.eje.Entity; }
			set { this.eje.Entity = value; }
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
			Context.Instancia.Contenidos.Attach(this);
		}

		public bool Equals(Contenido other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.AnoNumero.Equals(AnoNumero) && other.TipoEducacionCodigo.Equals(TipoEducacionCodigo) && other.SectorId.Equals(SectorId) && other.EjeId.Equals(EjeId) && other.GradoCodigo.Equals(GradoCodigo) && other.Id.Equals(Id);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(Contenido)) return false;
			return Equals((Contenido)obj);
		}

		public override int GetHashCode()
		{
			return this.AnoNumero.GetHashCode() ^ this.TipoEducacionCodigo.GetHashCode() ^ this.SectorId.GetHashCode() ^ this.EjeId.GetHashCode() ^ this.GradoCodigo.GetHashCode() ^ this.Id.GetHashCode();
		}
	}
}