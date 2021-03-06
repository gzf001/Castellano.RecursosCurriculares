using System;
using System.Collections.Generic;
using System.Data.Linq;
namespace RecursoCurricular.BaseCurricular
{
	[Serializable]
	public partial class UnidadSubHabilidad : IEquatable<UnidadSubHabilidad>
	{
		public UnidadSubHabilidad()
		{
			this.subHabilidad = default(EntityRef<RecursoCurricular.BaseCurricular.SubHabilidad>);
			this.unidad = default(EntityRef<RecursoCurricular.BaseCurricular.Unidad>);
		}

		public Int32 TipoEducacionCodigo { get; set; }

		public Int32 AnoNumero { get; set; }

		public Int32 GradoCodigo { get; set; }

		public Guid SectorId { get; set; }

		public Guid UnidadId { get; set; }

		public Guid HabilidadId { get; set; }

		public Guid SubHabilidadId { get; set; }

		[NonSerialized]
		private EntityRef<RecursoCurricular.BaseCurricular.SubHabilidad> subHabilidad;
		public RecursoCurricular.BaseCurricular.SubHabilidad SubHabilidad
		{
			get { return this.subHabilidad.Entity; }
			set { this.subHabilidad.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<RecursoCurricular.BaseCurricular.Unidad> unidad;
		public RecursoCurricular.BaseCurricular.Unidad Unidad
		{
			get { return this.unidad.Entity; }
			set { this.unidad.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.UnidadSubHabilidades.Attach(this);
		}

		public bool Equals(UnidadSubHabilidad other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.TipoEducacionCodigo.Equals(TipoEducacionCodigo) && other.AnoNumero.Equals(AnoNumero) && other.GradoCodigo.Equals(GradoCodigo) && other.SectorId.Equals(SectorId) && other.UnidadId.Equals(UnidadId) && other.HabilidadId.Equals(HabilidadId) && other.SubHabilidadId.Equals(SubHabilidadId);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(UnidadSubHabilidad)) return false;
			return Equals((UnidadSubHabilidad)obj);
		}

		public override int GetHashCode()
		{
			return this.TipoEducacionCodigo.GetHashCode() ^ this.AnoNumero.GetHashCode() ^ this.GradoCodigo.GetHashCode() ^ this.SectorId.GetHashCode() ^ this.UnidadId.GetHashCode() ^ this.HabilidadId.GetHashCode() ^ this.SubHabilidadId.GetHashCode();
		}
    }
}