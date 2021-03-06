using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
namespace RecursoCurricular.BaseCurricular
{
    [Serializable]
    public partial class AmbitoExperienciaAprendizaje : IEquatable<AmbitoExperienciaAprendizaje>
    {
        public AmbitoExperienciaAprendizaje()
        {
            this.anio = default(EntityRef<RecursoCurricular.Anio>);
        }

        public Int32 AnoNumero { get; set; }

        public Int32 Codigo { get; set; }

        [Display(Name = "Ámbito de aprendizaje:")]
        public String Nombre { get; set; }

        [NonSerialized]
        private EntityRef<RecursoCurricular.Anio> anio;
        public RecursoCurricular.Anio Anio
        {
            get { return this.anio.Entity; }
            set { this.anio.Entity = value; }
        }

        public void Attach()
        {
            Context.Instancia.AmbitoExperienciaAprendizajes.Attach(this);
        }

        public bool Equals(AmbitoExperienciaAprendizaje other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.AnoNumero.Equals(AnoNumero) && other.Codigo.Equals(Codigo);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(AmbitoExperienciaAprendizaje)) return false;
            return Equals((AmbitoExperienciaAprendizaje)obj);
        }

        public override int GetHashCode()
        {
            return this.AnoNumero.GetHashCode() ^ this.Codigo.GetHashCode();
        }
    }
}