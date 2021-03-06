using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
namespace RecursoCurricular.BaseCurricular
{
    [Serializable]
    public partial class Indicador : IEquatable<Indicador>
    {
        public Indicador()
        {
            this.Id = Guid.NewGuid();
            this.anio = default(EntityRef<RecursoCurricular.Anio>);
            this.grado = default(EntityRef<RecursoCurricular.Educacion.Grado>);
            this.objetivoAprendizaje = default(EntityRef<RecursoCurricular.BaseCurricular.ObjetivoAprendizaje>);
            this.sector = default(EntityRef<RecursoCurricular.Educacion.Sector>);
            this.tipoEducacion = default(EntityRef<RecursoCurricular.Educacion.TipoEducacion>);
        }

        public Int32 TipoEducacionCodigo { get; set; }

        public Int32 AnoNumero { get; set; }

        public Int32 GradoCodigo { get; set; }

        public Guid SectorId { get; set; }

        public Guid EjeId { get; set; }

        public Guid ObjetivoAprendizajeId { get; set; }

        public Guid Id { get; set; }

        [Display(Name = "Número:")]
        [Required(ErrorMessage = "El número es requerido")]
        public Int32 Numero { get; set; }

        [Display(Name = "Indicador:")]
        [Required(ErrorMessage = "El número del indicador es requerido")]
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
        private EntityRef<RecursoCurricular.BaseCurricular.ObjetivoAprendizaje> objetivoAprendizaje;
        public RecursoCurricular.BaseCurricular.ObjetivoAprendizaje ObjetivoAprendizaje
        {
            get { return this.objetivoAprendizaje.Entity; }
            set { this.objetivoAprendizaje.Entity = value; }
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
            Context.Instancia.Indicadores.Attach(this);
        }

        public bool Equals(Indicador other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.TipoEducacionCodigo.Equals(TipoEducacionCodigo) && other.AnoNumero.Equals(AnoNumero) && other.GradoCodigo.Equals(GradoCodigo) && other.SectorId.Equals(SectorId) && other.EjeId.Equals(EjeId) && other.ObjetivoAprendizajeId.Equals(ObjetivoAprendizajeId) && other.Id.Equals(Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Indicador)) return false;
            return Equals((Indicador)obj);
        }

        public override int GetHashCode()
        {
            return this.TipoEducacionCodigo.GetHashCode() ^ this.AnoNumero.GetHashCode() ^ this.GradoCodigo.GetHashCode() ^ this.SectorId.GetHashCode() ^ this.EjeId.GetHashCode() ^ this.ObjetivoAprendizajeId.GetHashCode() ^ this.Id.GetHashCode();
        }
    }
}