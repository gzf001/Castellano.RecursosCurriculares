using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
namespace RecursoCurricular.BaseCurricular
{
    [Serializable]
    public partial class NucleoAprendizaje : IEquatable<NucleoAprendizaje>
    {
        public NucleoAprendizaje()
        {
            this.Id = Guid.NewGuid();
            this.ambitoExperienciaAprendizaje = default(EntityRef<RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje>);
            this.anio = default(EntityRef<RecursoCurricular.Anio>);
        }

        public Int32 AnoNumero { get; set; }

        public Int32 AmbitoExperienciaAprendizajeCodigo { get; set; }

        public Guid Id { get; set; }

        public Int32 Numero { get; set; }

        [Display(Name = "Núcleo:")]
        [Required(ErrorMessage = "El núcleo es requerido")]
        public String Nombre { get; set; }

        [NonSerialized]
        private EntityRef<RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje> ambitoExperienciaAprendizaje;
        public RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje AmbitoExperienciaAprendizaje
        {
            get { return this.ambitoExperienciaAprendizaje.Entity; }
            set { this.ambitoExperienciaAprendizaje.Entity = value; }
        }

        [NonSerialized]
        private EntityRef<RecursoCurricular.Anio> anio;
        public RecursoCurricular.Anio Anio
        {
            get { return this.anio.Entity; }
            set { this.anio.Entity = value; }
        }

        public void Attach()
        {
            Context.Instancia.NucleoAprendizajes.Attach(this);
        }

        public bool Equals(NucleoAprendizaje other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.AnoNumero.Equals(AnoNumero) && other.AmbitoExperienciaAprendizajeCodigo.Equals(AmbitoExperienciaAprendizajeCodigo) && other.Id.Equals(Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(NucleoAprendizaje)) return false;
            return Equals((NucleoAprendizaje)obj);
        }

        public override int GetHashCode()
        {
            return this.AnoNumero.GetHashCode() ^ this.AmbitoExperienciaAprendizajeCodigo.GetHashCode() ^ this.Id.GetHashCode();
        }
    }
}