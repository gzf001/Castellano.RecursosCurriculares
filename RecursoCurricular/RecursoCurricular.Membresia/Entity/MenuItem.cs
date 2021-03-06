using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
namespace RecursoCurricular.Membresia
{
    [Serializable]
    public partial class MenuItem : IEquatable<MenuItem>
    {
        public MenuItem()
        {
            this.Id = Guid.NewGuid();
            this.aplicacion = default(EntityRef<RecursoCurricular.Membresia.Aplicacion>);
            this.padre = default(EntityRef<RecursoCurricular.Membresia.MenuItem>);
        }

        [Display(Name = "Aplicación:")]
        public Guid AplicacionId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El id del ítem de menú es requerido")]
        public Guid Id { get; set; }

        public Nullable<Guid> MenuItemId { get; set; }

        [Display(Name = "Nombre:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El nombre del ítem de menú es requerido")]
        public String Nombre { get; set; }

        [Display(Name = "Información:")]
        public String Informacion { get; set; }

        [Display(Name = "Icono:")]
        public String Icono { get; set; }

        [Display(Name = "URL:")]
        public String Url { get; set; }

        [Display(Name = "Visible:")]
        public Boolean Visible { get; set; }

        [Display(Name = "Orden:")]
        public Int32 Orden { get; set; }

        [NonSerialized]
        private EntityRef<RecursoCurricular.Membresia.Aplicacion> aplicacion;
        public RecursoCurricular.Membresia.Aplicacion Aplicacion
        {
            get { return this.aplicacion.Entity; }
            set { this.aplicacion.Entity = value; }
        }

        [NonSerialized]
        private EntityRef<RecursoCurricular.Membresia.MenuItem> padre;
        public RecursoCurricular.Membresia.MenuItem Padre
        {
            get { return this.padre.Entity; }
            set { this.padre.Entity = value; }
        }

        public void Attach()
        {
            Context.Instancia.MenuItemes.Attach(this);
        }

        public bool Equals(MenuItem other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.AplicacionId.Equals(AplicacionId) && other.Id.Equals(Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(MenuItem)) return false;
            return Equals((MenuItem)obj);
        }

        public override int GetHashCode()
        {
            return this.AplicacionId.GetHashCode() ^ this.Id.GetHashCode();
        }
    }
}