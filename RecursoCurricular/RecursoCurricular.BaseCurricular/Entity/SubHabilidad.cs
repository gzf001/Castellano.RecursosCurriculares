using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
namespace RecursoCurricular.BaseCurricular
{
    [Serializable]
    public partial class SubHabilidad : IEquatable<SubHabilidad>
    {
        public SubHabilidad()
        {
            this.Id = Guid.NewGuid();
            this.anio = default(EntityRef<RecursoCurricular.Anio>);
            this.grado = default(EntityRef<RecursoCurricular.Educacion.Grado>);
            this.habilidad = default(EntityRef<RecursoCurricular.BaseCurricular.Habilidad>);
            this.sector = default(EntityRef<RecursoCurricular.Educacion.Sector>);
            this.tipoEducacion = default(EntityRef<RecursoCurricular.Educacion.TipoEducacion>);
        }

        public Int32 TipoEducacionCodigo { get; set; }

        public Int32 AnoNumero { get; set; }

        public Int32 GradoCodigo { get; set; }

        public Guid HabilidadId { get; set; }

        public Guid SectorId { get; set; }

        public Guid Id { get; set; }

        [Display(Name = "Número:")]
        [Required(ErrorMessage = "El número es requerido")]
        public Int32 Numero { get; set; }

        [Display(Name = "Habilidad específica:")]
        [Required(ErrorMessage = "La habilidad específica es requerida")]
        public String Descripcion { get; set; }

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
        private EntityRef<RecursoCurricular.BaseCurricular.Habilidad> habilidad;
        public RecursoCurricular.BaseCurricular.Habilidad Habilidad
        {
            get { return this.habilidad.Entity; }
            set { this.habilidad.Entity = value; }
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
            Context.Instancia.SubHabilidades.Attach(this);
        }

        public bool Equals(SubHabilidad other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.TipoEducacionCodigo.Equals(TipoEducacionCodigo) && other.AnoNumero.Equals(AnoNumero) && other.GradoCodigo.Equals(GradoCodigo) && other.HabilidadId.Equals(HabilidadId) && other.SectorId.Equals(SectorId) && other.Id.Equals(Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(SubHabilidad)) return false;
            return Equals((SubHabilidad)obj);
        }

        public override int GetHashCode()
        {
            return this.TipoEducacionCodigo.GetHashCode() ^ this.AnoNumero.GetHashCode() ^ this.GradoCodigo.GetHashCode() ^ this.HabilidadId.GetHashCode() ^ this.SectorId.GetHashCode() ^ this.Id.GetHashCode();
        }
    }
}