using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
namespace RecursoCurricular.Educacion
{
	[Serializable]
	public partial class Ciclo : IEquatable<Ciclo>
	{
		public Ciclo()
		{
		}

		public Int32 Codigo { get; set; }

        [Display(Name = "Ciclo:")]
        [Required(ErrorMessage = "El ciclo es requerido")]
        public String Nombre { get; set; }

		public void Attach()
		{
			Context.Instancia.Ciclos.Attach(this);
		}

		public bool Equals(Ciclo other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.Codigo.Equals(Codigo);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(Ciclo)) return false;
			return Equals((Ciclo)obj);
		}

		public override int GetHashCode()
		{
			return this.Codigo.GetHashCode();
		}
	}
}