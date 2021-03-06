using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
namespace RecursoCurricular.Membresia
{
	[Serializable]
	public partial class Rol : IEquatable<Rol>
	{
		public Rol()
		{
			this.Id = Guid.NewGuid();
		}

		public Guid Id { get; set; }

        [Display(Name = "Rol:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar el nombre")]
        public String Nombre { get; set; }

        [Display(Name = "Clave:")]
        public String Clave { get; set; }

		public void Attach()
		{
			Context.Instancia.Roles.Attach(this);
		}

		public bool Equals(Rol other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.Id.Equals(Id);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(Rol)) return false;
			return Equals((Rol)obj);
		}

		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}
	}
}