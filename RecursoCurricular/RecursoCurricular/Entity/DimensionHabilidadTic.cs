using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
namespace RecursoCurricular
{
    [Serializable]
    public partial class DimensionHabilidadTic : IEquatable<DimensionHabilidadTic>
    {
        public DimensionHabilidadTic()
        {
            this.Id = Guid.NewGuid();
            this.anio = default(EntityRef<RecursoCurricular.Anio>);
        }

        [Required(ErrorMessage = "El id es requerido")]
        public Guid Id { get; set; }

        public Int32 AnoNumero { get; set; }

        [Display(Name = "Número:")]
        [Required(ErrorMessage = "El nombre es requerido")]
        public Int32 Numero { get; set; }

        [Display(Name = "Dimensión:")]
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

        public void Attach()
        {
            Context.Instancia.DimensionHabilidadTices.Attach(this);
        }

        public bool Equals(DimensionHabilidadTic other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id.Equals(Id) && other.AnoNumero.Equals(AnoNumero);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(DimensionHabilidadTic)) return false;
            return Equals((DimensionHabilidadTic)obj);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode() ^ this.AnoNumero.GetHashCode();
        }
    }
}