using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
namespace RecursoCurricular
{
    [Serializable]
    public partial class HabilidadTic : IEquatable<HabilidadTic>
    {
        public HabilidadTic()
        {
            this.Id = Guid.NewGuid();
            this.anio = default(EntityRef<RecursoCurricular.Anio>);
            this.dimensionHabilidadTIC = default(EntityRef<RecursoCurricular.DimensionHabilidadTic>);
        }

        public Guid Id { get; set; }

        public Guid DimensionHabilidadTicId { get; set; }

        public Int32 AnoNumero { get; set; }

        [Display(Name = "Número:")]
        [Required(ErrorMessage = "El número de habilidad es requerido")]
        public Int32 Numero { get; set; }

        [Display(Name = "Habilidad:")]
        [Required(ErrorMessage = "El nombre de habilidad es requerido")]
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
        private EntityRef<RecursoCurricular.DimensionHabilidadTic> dimensionHabilidadTIC;
        public RecursoCurricular.DimensionHabilidadTic DimensionHabilidadTIC
        {
            get { return this.dimensionHabilidadTIC.Entity; }
            set { this.dimensionHabilidadTIC.Entity = value; }
        }

        public void Attach()
        {
            Context.Instancia.HabilidadTices.Attach(this);
        }

        public bool Equals(HabilidadTic other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id.Equals(Id) && other.DimensionHabilidadTicId.Equals(DimensionHabilidadTicId) && other.AnoNumero.Equals(AnoNumero);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(HabilidadTic)) return false;
            return Equals((HabilidadTic)obj);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode() ^ this.DimensionHabilidadTicId.GetHashCode() ^ this.AnoNumero.GetHashCode();
        }
    }
}