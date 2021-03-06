using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
namespace RecursoCurricular.BaseCurricular
{
    [Serializable]
    public partial class Habilidad : IEquatable<Habilidad>
    {
        public Habilidad()
        {
			this.Id = Guid.NewGuid();
            this.anio = default(EntityRef<RecursoCurricular.Anio>);
            this.sector = default(EntityRef<RecursoCurricular.Educacion.Sector>);
            this.tipoEducacion = default(EntityRef<RecursoCurricular.Educacion.TipoEducacion>);
        }

        public Guid Id { get; set; }

        public Int32 TipoEducacionCodigo { get; set; }

        public Int32 AnoNumero { get; set; }

        public Guid SectorId { get; set; }

        [Display(Name = "Número:")]
        [Required(ErrorMessage = "Debe ingresar el número de habilidad")]
        public Int32 Numero { get; set; }

        [Display(Name = "Habilidad:")]
        [Required(ErrorMessage = "Debe ingresar la descripción")]
        public String Descripcion { get; set; }

        [NonSerialized]
        private EntityRef<RecursoCurricular.Anio> anio;
        public RecursoCurricular.Anio Anio
        {
            get { return this.anio.Entity; }
            set { this.anio.Entity = value; }
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
            Context.Instancia.Habilidades.Attach(this);
        }

        public bool Equals(Habilidad other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id.Equals(Id) && other.TipoEducacionCodigo.Equals(TipoEducacionCodigo) && other.AnoNumero.Equals(AnoNumero) && other.SectorId.Equals(SectorId);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Habilidad)) return false;
            return Equals((Habilidad)obj);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode() ^ this.TipoEducacionCodigo.GetHashCode() ^ this.AnoNumero.GetHashCode() ^ this.SectorId.GetHashCode();
        }
    }
}