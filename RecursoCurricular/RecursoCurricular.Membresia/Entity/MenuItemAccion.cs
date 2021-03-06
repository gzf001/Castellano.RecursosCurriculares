using System;
using System.Data.Linq;
namespace RecursoCurricular.Membresia
{
	[Serializable]
	public partial class MenuItemAccion : IEquatable<MenuItemAccion>
	{
		public MenuItemAccion()
		{
			this.accion = default(EntityRef<RecursoCurricular.Membresia.Accion>);
			this.menuItem = default(EntityRef<RecursoCurricular.Membresia.MenuItem>);
		}

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

		public void Attach()
		{
			Context.Instancia.MenuItemAcciones.Attach(this);
		}

		public bool Equals(MenuItemAccion other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.AplicacionId.Equals(AplicacionId) && other.MenuItemId.Equals(MenuItemId) && other.AccionCodigo.Equals(AccionCodigo);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(MenuItemAccion)) return false;
			return Equals((MenuItemAccion)obj);
		}

		public override int GetHashCode()
		{
			return this.AplicacionId.GetHashCode() ^ this.MenuItemId.GetHashCode() ^ this.AccionCodigo.GetHashCode();
		}
	}
}