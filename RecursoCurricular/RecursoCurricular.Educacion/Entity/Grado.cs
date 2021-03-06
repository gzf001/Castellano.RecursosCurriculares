using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
namespace RecursoCurricular.Educacion
{
    [Serializable]
    public partial class Grado : IEquatable<Grado>
    {
        public Grado()
        {
            this.siguienteGrado = default(EntityRef<RecursoCurricular.Educacion.Grado>);
            this.siguienteTipoEducacion = default(EntityRef<RecursoCurricular.Educacion.TipoEducacion>);
            this.tipoEducacion = default(EntityRef<RecursoCurricular.Educacion.TipoEducacion>);
        }

        public Int32 TipoEducacionCodigo { get; set; }

        public Int32 Codigo { get; set; }

        [Display(Name = "Grado:")]
        [Required(ErrorMessage = "El grado es requerido")]
        public String Nombre { get; set; }

        public Nullable<Int32> SiguienteTipoEducacionCodigo { get; set; }

        public Nullable<Int32> SiguienteGradoCodigo { get; set; }

        [NonSerialized]
        private EntityRef<RecursoCurricular.Educacion.Grado> siguienteGrado;
        public RecursoCurricular.Educacion.Grado SiguienteGrado
        {
            get { return this.siguienteGrado.Entity; }
            set { this.siguienteGrado.Entity = value; }
        }

        [NonSerialized]
        private EntityRef<RecursoCurricular.Educacion.TipoEducacion> siguienteTipoEducacion;
        public RecursoCurricular.Educacion.TipoEducacion SiguienteTipoEducacion
        {
            get { return this.siguienteTipoEducacion.Entity; }
            set { this.siguienteTipoEducacion.Entity = value; }
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
            Context.Instancia.Grados.Attach(this);
        }

        public bool Equals(Grado other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.TipoEducacionCodigo.Equals(TipoEducacionCodigo) && other.Codigo.Equals(Codigo);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Grado)) return false;
            return Equals((Grado)obj);
        }

        public override int GetHashCode()
        {
            return this.TipoEducacionCodigo.GetHashCode() ^ this.Codigo.GetHashCode();
        }
    }
}