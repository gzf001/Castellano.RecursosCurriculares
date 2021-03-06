using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
namespace RecursoCurricular.Educacion
{
    [Serializable]
    public partial class Sector : IEquatable<Sector>
    {
        public Sector()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        [Display(Name = "Sector:")]
        [Required(ErrorMessage = "El sector es requerido")]
        public String Nombre { get; set; }

        public Int32 Orden { get; set; }

        public void Attach()
        {
            Context.Instancia.Sectores.Attach(this);
        }

        public bool Equals(Sector other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id.Equals(Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Sector)) return false;
            return Equals((Sector)obj);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}