using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
namespace RecursoCurricular.BaseCurricular
{
    [Serializable]
    public partial class ObjetivoAprendizaje : IEquatable<ObjetivoAprendizaje>
    {
        public ObjetivoAprendizaje()
        {
            this.Id = Guid.NewGuid();
            this.anio = default(EntityRef<RecursoCurricular.Anio>);
            this.eje = default(EntityRef<RecursoCurricular.BaseCurricular.Eje>);
            this.grado = default(EntityRef<RecursoCurricular.Educacion.Grado>);
            this.sector = default(EntityRef<RecursoCurricular.Educacion.Sector>);
            this.tipoEducacion = default(EntityRef<RecursoCurricular.Educacion.TipoEducacion>);
        }

        [Display(Name = "Tipo de educación:")]
        [Required(ErrorMessage = "El tipo de educación es requerido")]
        public Int32 TipoEducacionCodigo { get; set; }

        public Int32 AnoNumero { get; set; }

        [Display(Name = "Grado:")]
        [Required(ErrorMessage = "El grado es requerido")]
        public Int32 GradoCodigo { get; set; }

        [Display(Name = "Sector:")]
        [Required(ErrorMessage = "El sector es requerido")]
        public Guid SectorId { get; set; }

        [Display(Name = "Eje:")]
        [Required(ErrorMessage = "El eje es requerido")]
        public Guid EjeId { get; set; }

        [Required(ErrorMessage = "El id es requerido")]
        public Guid Id { get; set; }

        [Display(Name = "Número:")]
        [Required(ErrorMessage = "El número es requerido")]
        public Int32 Numero { get; set; }

        [Display(Name = "Objetivo de aprendizaje:")]
        [Required(ErrorMessage = "La descripción del objetivo es requerida")]
        public String Descripcion { get; set; }

        [NonSerialized]
        private EntityRef<RecursoCurricular.Anio> anio;
        public RecursoCurricular.Anio Anio
        {
            get { return this.anio.Entity; }
            set { this.anio.Entity = value; }
        }

        [NonSerialized]
        private EntityRef<RecursoCurricular.BaseCurricular.Eje> eje;
        public RecursoCurricular.BaseCurricular.Eje Eje
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
            Context.Instancia.ObjetivoAprendizajes.Attach(this);
        }

        public bool Equals(ObjetivoAprendizaje other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.TipoEducacionCodigo.Equals(TipoEducacionCodigo) && other.AnoNumero.Equals(AnoNumero) && other.GradoCodigo.Equals(GradoCodigo) && other.SectorId.Equals(SectorId) && other.EjeId.Equals(EjeId) && other.Id.Equals(Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(ObjetivoAprendizaje)) return false;
            return Equals((ObjetivoAprendizaje)obj);
        }

        public override int GetHashCode()
        {
            return this.TipoEducacionCodigo.GetHashCode() ^ this.AnoNumero.GetHashCode() ^ this.GradoCodigo.GetHashCode() ^ this.SectorId.GetHashCode() ^ this.EjeId.GetHashCode() ^ this.Id.GetHashCode();
        }
    }
}