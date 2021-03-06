using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
namespace RecursoCurricular.Membresia
{
	[Serializable]
	public partial class Aplicacion : IEquatable<Aplicacion>
	{
		public Aplicacion()
		{
			this.Id = Guid.NewGuid();
			this.inicio = default(EntityRef<RecursoCurricular.Membresia.MenuItem>);
		}

		public Guid Id { get; set; }

		public Nullable<Guid> MenuItemId { get; set; }

        [Display(Name = "Nombre:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar el nombre")]
        public String Nombre { get; set; }

        [Display(Name = "Clave:")]
        public String Clave { get; set; }

        [Display(Name = "Icono:")]
        public String Fa_Icon { get; set; }

        [Display(Name = "Orden:")]
        public Byte Orden { get; set; }

		[NonSerialized]
		private EntityRef<RecursoCurricular.Membresia.MenuItem> inicio;
		public RecursoCurricular.Membresia.MenuItem Inicio
		{
			get { return this.inicio.Entity; }
			set { this.inicio.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.Aplicaciones.Attach(this);
		}

		public bool Equals(Aplicacion other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.Id.Equals(Id);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(Aplicacion)) return false;
			return Equals((Aplicacion)obj);
		}

		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}
	}
}