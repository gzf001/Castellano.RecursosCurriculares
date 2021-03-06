using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
namespace RecursoCurricular.BaseCurricular
{
    [Serializable]
    public partial class EjeParvulo : IEquatable<EjeParvulo>
    {
        public EjeParvulo()
        {
            this.Id = Guid.NewGuid();
            this.ambitoExperienciaAprendizaje = default(EntityRef<RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje>);
            this.anio = default(EntityRef<RecursoCurricular.Anio>);
            this.ciclo = default(EntityRef<RecursoCurricular.Educacion.Ciclo>);
            this.nucleoAprendizaje = default(EntityRef<RecursoCurricular.BaseCurricular.NucleoAprendizaje>);
        }

        public Int32 AnoNumero { get; set; }

        public Int32 AmbitoExperienciaAprendizajeCodigo { get; set; }

        public Guid NucleoId { get; set; }

        public Int32 CicloCodigo { get; set; }

        public Guid Id { get; set; }

        public Int32 Numero { get; set; }

        [Display(Name = "Eje:")]
        [Required(ErrorMessage = "El eje es requerido")]
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

        [NonSerialized]
        private EntityRef<RecursoCurricular.Educacion.Ciclo> ciclo;
        public RecursoCurricular.Educacion.Ciclo Ciclo
        {
            get { return this.ciclo.Entity; }
            set { this.ciclo.Entity = value; }
        }

        [NonSerialized]
        private EntityRef<RecursoCurricular.BaseCurricular.NucleoAprendizaje> nucleoAprendizaje;
        public RecursoCurricular.BaseCurricular.NucleoAprendizaje NucleoAprendizaje
        {
            get { return this.nucleoAprendizaje.Entity; }
            set { this.nucleoAprendizaje.Entity = value; }
        }

        public void Attach()
        {
            Context.Instancia.EjeParvulos.Attach(this);
        }

        public bool Equals(EjeParvulo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.AnoNumero.Equals(AnoNumero) && other.AmbitoExperienciaAprendizajeCodigo.Equals(AmbitoExperienciaAprendizajeCodigo) && other.NucleoId.Equals(NucleoId) && other.CicloCodigo.Equals(CicloCodigo) && other.Id.Equals(Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(EjeParvulo)) return false;
            return Equals((EjeParvulo)obj);
        }

        public override int GetHashCode()
        {
            return this.AnoNumero.GetHashCode() ^ this.AmbitoExperienciaAprendizajeCodigo.GetHashCode() ^ this.NucleoId.GetHashCode() ^ this.CicloCodigo.GetHashCode() ^ this.Id.GetHashCode();
        }
    }
}