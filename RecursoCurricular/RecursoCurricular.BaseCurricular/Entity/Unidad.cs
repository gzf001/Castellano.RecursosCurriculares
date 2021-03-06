using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
namespace RecursoCurricular.BaseCurricular
{
    [Serializable]
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

        public Int32 TipoEducacionCodigo { get; set; }

        public Int32 AnoNumero { get; set; }

        public Int32 GradoCodigo { get; set; }

        public Guid SectorId { get; set; }

        public Guid Id { get; set; }

        [Display(Name = "Propósito:")]
        public String Proposito { get; set; }

        [Display(Name = "Conocimientos previos:")]
        public String ConocimientoPrevio { get; set; }

        [Display(Name = "Palabras claves:")]
        public String PalabraClave { get; set; }

        [Required(ErrorMessage = "El número de la unidad es requerido")]
        [Display(Name = "Número:")]
        public Int32 Numero { get; set; }

        [Required(ErrorMessage = "El nombre de la unidad es requerido")]
        [Display(Name = "Unidad:")]
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
            RecursoCurricular.BaseCurricular.Unidad unidad = RecursoCurricular.BaseCurricular.Unidad.Get(this.TipoEducacionCodigo, this.AnoNumero, this.GradoCodigo, this.SectorId, this.Id);

            foreach (System.Reflection.PropertyInfo property in unidad.GetType().GetProperties())
            {
                this.GetType().GetProperty(property.Name).SetValue(this, property.GetValue(unidad));
            }
        }

        public bool Equals(Unidad other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.TipoEducacionCodigo.Equals(TipoEducacionCodigo) && other.AnoNumero.Equals(AnoNumero) && other.GradoCodigo.Equals(GradoCodigo) && other.SectorId.Equals(SectorId) && other.Id.Equals(Id);
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
            return this.TipoEducacionCodigo.GetHashCode() ^ this.AnoNumero.GetHashCode() ^ this.GradoCodigo.GetHashCode() ^ this.SectorId.GetHashCode() ^ this.Id.GetHashCode();
        }
    }
}