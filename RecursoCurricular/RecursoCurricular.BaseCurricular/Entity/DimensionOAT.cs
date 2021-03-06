using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
namespace RecursoCurricular.BaseCurricular
{
	[Serializable]
	public partial class DimensionOAT : IEquatable<DimensionOAT>
	{
		public DimensionOAT()
		{
			this.Id = Guid.NewGuid();
			this.anio = default(EntityRef<RecursoCurricular.Anio>);
		}

        [Required(ErrorMessage = "El id es requerido")]
        public Guid Id { get; set; }

		public Int32 AnoNumero { get; set; }

        [Display(Name = "Número:")]
        [Required(ErrorMessage = "El nombre es requerido")]
        public Int32 Numero { get; set; }

        [Display(Name = "Dimensión:")]
        [Required(ErrorMessage = "El nombre es requerido")]
        public String Nombre { get; set; }

        [Display(Name = "Descripción:")]
        public String Descripcion { get; set; }

		[NonSerialized]
		private EntityRef<RecursoCurricular.Anio> anio;
		public RecursoCurricular.Anio Anio
		{
			get { return this.anio.Entity; }
			set { this.anio.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.DimensionOATes.Attach(this);
		}

		public bool Equals(DimensionOAT other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.Id.Equals(Id) && other.AnoNumero.Equals(AnoNumero);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(DimensionOAT)) return false;
			return Equals((DimensionOAT)obj);
		}

		public override int GetHashCode()
		{
			return this.Id.GetHashCode() ^ this.AnoNumero.GetHashCode();
		}
    }
}