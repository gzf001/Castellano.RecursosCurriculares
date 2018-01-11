using System;
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
			this.rol = default(EntityRef<RecursoCurricular.Membresia.Rol>);
		}

		public Guid RolId { get; set; }

		public Guid AplicacionId { get; set; }

		public Guid MenuItemId { get; set; }

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