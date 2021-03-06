using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
namespace RecursoCurricular.Membresia
{
	[Serializable]
	public partial class RolAccion : IEquatable<RolAccion>
	{
		public RolAccion()
		{
			this.accion = default(EntityRef<RecursoCurricular.Membresia.Accion>);
			this.menuItem = default(EntityRef<RecursoCurricular.Membresia.MenuItem>);
			this.menuItemAccion = default(EntityRef<RecursoCurricular.Membresia.MenuItemAccion>);
			this.rol = default(EntityRef<RecursoCurricular.Membresia.Rol>);
		}

        [Display(Name = "Rol:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar el rol")]
        public Guid RolId { get; set; }

        [Display(Name = "Aplicación:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar la aplicación")]
        public Guid AplicacionId { get; set; }

        [Display(Name = "Ítem de menú:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar el ítem de menú")]
        public Guid MenuItemId { get; set; }

        [Display(Name = "Acción:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar la acción")]
        public Int32 AccionCodigo { get; set; }

		[NonSerialized]
		private EntityRef<RecursoCurricular.Membresia.Accion> accion;
		public RecursoCurricular.Membresia.Accion Accion
		{
			get { return this.accion.Entity; }
			set { this.accion.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<RecursoCurricular.Membresia.MenuItem> menuItem;
		public RecursoCurricular.Membresia.MenuItem MenuItem
		{
			get { return this.menuItem.Entity; }
			set { this.menuItem.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<RecursoCurricular.Membresia.MenuItemAccion> menuItemAccion;
		public RecursoCurricular.Membresia.MenuItemAccion MenuItemAccion
		{
			get { return this.menuItemAccion.Entity; }
			set { this.menuItemAccion.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<RecursoCurricular.Membresia.Rol> rol;
		public RecursoCurricular.Membresia.Rol Rol
		{
			get { return this.rol.Entity; }
			set { this.rol.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.RolAcciones.Attach(this);
		}

		public bool Equals(RolAccion other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.RolId.Equals(RolId) && other.AplicacionId.Equals(AplicacionId) && other.MenuItemId.Equals(MenuItemId) && other.AccionCodigo.Equals(AccionCodigo);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(RolAccion)) return false;
			return Equals((RolAccion)obj);
		}

		public override int GetHashCode()
		{
			return this.RolId.GetHashCode() ^ this.AplicacionId.GetHashCode() ^ this.MenuItemId.GetHashCode() ^ this.AccionCodigo.GetHashCode();
		}
	}
}