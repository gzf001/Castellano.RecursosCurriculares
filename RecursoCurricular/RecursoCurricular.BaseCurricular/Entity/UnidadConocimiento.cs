using System;
using System.Data.Linq;
namespace RecursoCurricular.BaseCurricular
{
	[Serializable]
	public partial class UnidadConocimiento : IEquatable<UnidadConocimiento>
	{
		public UnidadConocimiento()
		{
			this.anio = default(EntityRef<RecursoCurricular.Anio>);
			this.conocimiento = default(EntityRef<RecursoCurricular.BaseCurricular.Conocimiento>);
			this.unidad = default(EntityRef<RecursoCurricular.BaseCurricular.Unidad>);
		}

		public Int32 TipoEducacionCodigo { get; set; }

		public Int32 AnoNumero { get; set; }

		public Int32 GradoCodigo { get; set; }

		public Guid SectorId { get; set; }

		public Guid UnidadId { get; set; }

		public Guid ConocimientoId { get; set; }

		[NonSerialized]
		private EntityRef<RecursoCurricular.Anio> anio;
		public RecursoCurricular.Anio Anio
		{
			get { return this.anio.Entity; }
			set { this.anio.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<RecursoCurricular.BaseCurricular.Conocimiento> conocimiento;
		public RecursoCurricular.BaseCurricular.Conocimiento Conocimiento
		{
			get { return this.conocimiento.Entity; }
			set { this.conocimiento.Entity = value; }
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
			Context.Instancia.UnidadConocimientos.Attach(this);
		}

		public bool Equals(UnidadConocimiento other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.TipoEducacionCodigo.Equals(TipoEducacionCodigo) && other.AnoNumero.Equals(AnoNumero) && other.GradoCodigo.Equals(GradoCodigo) && other.SectorId.Equals(SectorId) && other.UnidadId.Equals(UnidadId) && other.ConocimientoId.Equals(ConocimientoId);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(UnidadConocimiento)) return false;
			return Equals((UnidadConocimiento)obj);
		}

		public override int GetHashCode()
		{
			return this.TipoEducacionCodigo.GetHashCode() ^ this.AnoNumero.GetHashCode() ^ this.GradoCodigo.GetHashCode() ^ this.SectorId.GetHashCode() ^ this.UnidadId.GetHashCode() ^ this.ConocimientoId.GetHashCode();
		}
	}
}