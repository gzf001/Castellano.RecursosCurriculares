using System;
using System.Data.Linq;
namespace RecursoCurricular.BaseCurricular
{
	[Serializable]
	public partial class UnidadActitud : IEquatable<UnidadActitud>
	{
		public UnidadActitud()
		{
			this.actitud = default(EntityRef<RecursoCurricular.BaseCurricular.Actitud>);
			this.anio = default(EntityRef<RecursoCurricular.Anio>);
			this.unidad = default(EntityRef<RecursoCurricular.BaseCurricular.Unidad>);
		}

		public Int32 TipoEducacionCodigo { get; set; }

		public Int32 AnoNumero { get; set; }

		public Int32 GradoCodigo { get; set; }

		public Guid SectorId { get; set; }

		public Guid UnidadId { get; set; }

		public Guid ActitudId { get; set; }

		[NonSerialized]
		private EntityRef<RecursoCurricular.BaseCurricular.Actitud> actitud;
		public RecursoCurricular.BaseCurricular.Actitud Actitud
		{
			get { return this.actitud.Entity; }
			set { this.actitud.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<RecursoCurricular.Anio> anio;
		public RecursoCurricular.Anio Anio
		{
			get { return this.anio.Entity; }
			set { this.anio.Entity = value; }
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
			Context.Instancia.UnidadActitudes.Attach(this);
		}

		public bool Equals(UnidadActitud other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.TipoEducacionCodigo.Equals(TipoEducacionCodigo) && other.AnoNumero.Equals(AnoNumero) && other.GradoCodigo.Equals(GradoCodigo) && other.SectorId.Equals(SectorId) && other.UnidadId.Equals(UnidadId) && other.ActitudId.Equals(ActitudId);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(UnidadActitud)) return false;
			return Equals((UnidadActitud)obj);
		}

		public override int GetHashCode()
		{
			return this.TipoEducacionCodigo.GetHashCode() ^ this.AnoNumero.GetHashCode() ^ this.GradoCodigo.GetHashCode() ^ this.SectorId.GetHashCode() ^ this.UnidadId.GetHashCode() ^ this.ActitudId.GetHashCode();
		}
	}
}