using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
namespace RecursoCurricular.BaseCurricular
{
    [Serializable]
    public partial class ObjetivoAprendizajeTransversal : IEquatable<ObjetivoAprendizajeTransversal>
    {
        public ObjetivoAprendizajeTransversal()
        {
            this.Id = Guid.NewGuid();
            this.anio = default(EntityRef<RecursoCurricular.Anio>);
            this.dimensionOAT = default(EntityRef<RecursoCurricular.BaseCurricular.DimensionOAT>);
        }

        [Display(Name = "Dimensión:")]
        [Required(ErrorMessage = "La dimensión es requerida")]
        public Guid DimensionOATId { get; set; }

        public Int32 AnoNumero { get; set; }

        public Guid Id { get; set; }

        [Display(Name = "Número:")]
        [Required(ErrorMessage = "El número es requerido")]
        public Int32 Numero { get; set; }

        [Display(Name = "Nombre:")]
        [Required(ErrorMessage = "El nombre es requerido")]
        public String Nombre { get; set; }

        [Display(Name = "Descripción:")]
        public String Descripcion { get; set; }

        [NonSerialized]
        private EntityRef<RecursoCurricular.Anio> anio;
        public RecursoCurricular.Anio Anio
        {
            get { return this.anio.Entity; }
            set { this.anio.Entity = value; }
        }

        [NonSerialized]
        private EntityRef<RecursoCurricular.BaseCurricular.DimensionOAT> dimensionOAT;
        public RecursoCurricular.BaseCurricular.DimensionOAT DimensionOAT
        {
            get { return this.dimensionOAT.Entity; }
            set { this.dimensionOAT.Entity = value; }
        }

        public void Attach()
        {
            Context.Instancia.ObjetivoAprendizajeTransversales.Attach(this);
        }

        public bool Equals(ObjetivoAprendizajeTransversal other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.DimensionOATId.Equals(DimensionOATId) && other.AnoNumero.Equals(AnoNumero) && other.Id.Equals(Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(ObjetivoAprendizajeTransversal)) return false;
            return Equals((ObjetivoAprendizajeTransversal)obj);
        }

        public override int GetHashCode()
        {
            return this.DimensionOATId.GetHashCode() ^ this.AnoNumero.GetHashCode() ^ this.Id.GetHashCode();
        }
    }
}