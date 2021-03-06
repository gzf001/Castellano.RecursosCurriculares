using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
using System.Runtime.Serialization;

namespace RecursoCurricular.RecursosCurriculares
{
	[Serializable]
    [DataContract]
	public partial class Unidad : IEquatable<Unidad>
	{
		public Unidad()
		{
			this.Id = Guid.NewGuid();
			this.anio = default(EntityRef<RecursoCurricular.Anio>);
			this.grado = default(EntityRef<RecursoCurricular.Educacion.Grado>);
			this.sector = default(EntityRef<RecursoCurricular.Educacion.Sector>);
			this.tipoEducacion = default(EntityRef<RecursoCurricular.Educacion.TipoEducacion>);
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
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El número de la unidad es requerido")]
        [Display(Name = "Número:")]
        [DataMember]
        public Int32 Numero { get; set; }

        [Required(ErrorMessage = "El nombre de la unidad es requerido")]
        [Display(Name = "Unidad:")]
        [DataMember]
        public String Nombre { get; set; }

		[NonSerialized]
		private EntityRef<RecursoCurricular.Anio> anio;
		public RecursoCurricular.Anio Anio
		{
			get { return this.anio.Entity; }
			set { this.anio.Entity = value; }
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
            RecursoCurricular.RecursosCurriculares.Unidad unidad = RecursoCurricular.RecursosCurriculares.Unidad.Get(this.TipoEducacionCodigo, this.AnoNumero, this.GradoCodigo, this.SectorId, this.Id);

            foreach (System.Reflection.PropertyInfo property in unidad.GetType().GetProperties())
            {
                this.GetType().GetProperty(property.Name).SetValue(this, property.GetValue(unidad));
            }
        }

		public bool Equals(Unidad other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.AnoNumero.Equals(AnoNumero) && other.TipoEducacionCodigo.Equals(TipoEducacionCodigo) && other.GradoCodigo.Equals(GradoCodigo) && other.SectorId.Equals(SectorId) && other.Id.Equals(Id);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(Unidad)) return false;
			return Equals((Unidad)obj);
		}

		public override int GetHashCode()
		{
			return this.AnoNumero.GetHashCode() ^ this.TipoEducacionCodigo.GetHashCode() ^ this.GradoCodigo.GetHashCode() ^ this.SectorId.GetHashCode() ^ this.Id.GetHashCode();
		}
	}
}