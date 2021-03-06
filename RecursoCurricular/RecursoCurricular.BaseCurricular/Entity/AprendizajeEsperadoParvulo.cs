using System;
using System.Collections.Generic;
using System.Data.Linq;
using RecursoCurricular.Educacion;
using System.ComponentModel.DataAnnotations;

namespace RecursoCurricular.BaseCurricular
{
    [Serializable]
    public partial class AprendizajeEsperadoParvulo : IEquatable<AprendizajeEsperadoParvulo>
    {
        public AprendizajeEsperadoParvulo()
        {
            this.ambitoExperienciaAprendizaje = default(EntityRef<RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje>);
            this.anio = default(EntityRef<RecursoCurricular.Anio>);
            this.ciclo = default(EntityRef<RecursoCurricular.Educacion.Ciclo>);
            this.ejeParvulo = default(EntityRef<RecursoCurricular.BaseCurricular.EjeParvulo>);
            this.nucleoAprendizaje = default(EntityRef<RecursoCurricular.BaseCurricular.NucleoAprendizaje>);
        }

        public Int32 AnoNumero { get; set; }

        public Int32 AmbitoExperienciaAprendizajeCodigo { get; set; }

        public Guid NucleoAprendizajeId { get; set; }

        public Int32 CicloCodigo { get; set; }

        public Guid Id { get; set; }

        public Nullable<Guid> EjeParvuloId { get; set; }

        [Display(Name = "Número:")]
        [Required(ErrorMessage = "El número de aprendizaje es requerido")]
        public Int32 Numero { get; set; }

        [Display(Name = "Descripción:")]
        [Required(ErrorMessage = "La descripción es requerida")]
        public String Descripcion { get; set; }

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
        private EntityRef<RecursoCurricular.BaseCurricular.EjeParvulo> ejeParvulo;
        public RecursoCurricular.BaseCurricular.EjeParvulo EjeParvulo
        {
            get { return this.ejeParvulo.Entity; }
            set { this.ejeParvulo.Entity = value; }
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
            Context.Instancia.AprendizajeEsperadoParvulos.Attach(this);
        }

        public bool Equals(AprendizajeEsperadoParvulo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.AnoNumero.Equals(AnoNumero) && other.AmbitoExperienciaAprendizajeCodigo.Equals(AmbitoExperienciaAprendizajeCodigo) && other.NucleoAprendizajeId.Equals(NucleoAprendizajeId) && other.CicloCodigo.Equals(CicloCodigo) && other.Id.Equals(Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(AprendizajeEsperadoParvulo)) return false;
            return Equals((AprendizajeEsperadoParvulo)obj);
        }

        public override int GetHashCode()
        {
            return this.AnoNumero.GetHashCode() ^ this.AmbitoExperienciaAprendizajeCodigo.GetHashCode() ^ this.NucleoAprendizajeId.GetHashCode() ^ this.CicloCodigo.GetHashCode() ^ this.Id.GetHashCode();
        }
    }
}