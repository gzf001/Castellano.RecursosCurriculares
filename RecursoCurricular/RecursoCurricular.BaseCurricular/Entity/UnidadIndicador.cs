using System;
using System.Data.Linq;
namespace RecursoCurricular.BaseCurricular
{
	[Serializable]
	public partial class UnidadIndicador : IEquatable<UnidadIndicador>
	{
		public UnidadIndicador()
		{
			this.indicador = default(EntityRef<RecursoCurricular.BaseCurricular.Indicador>);
			this.unidad = default(EntityRef<RecursoCurricular.BaseCurricular.Unidad>);
		}

		public Int32 TipoEducacionCodigo { get; set; }

		public Int32 AnoNumero { get; set; }

		public Int32 GradoCodigo { get; set; }

		public Guid SectorId { get; set; }

		public Guid UnidadId { get; set; }

		public Guid EjeId { get; set; }

		public Guid ObjetivoAprendizajeId { get; set; }

		public Guid IndicadorId { get; set; }

		public Int32 Orden { get; set; }

		[NonSerialized]
		private EntityRef<RecursoCurricular.BaseCurricular.Indicador> indicador;
		public RecursoCurricular.BaseCurricular.Indicador Indicador
		{
			get { return this.indicador.Entity; }
			set { this.indicador.Entity = value; }
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
			Context.Instancia.UnidadIndicadores.Attach(this);
		}

		public bool Equals(UnidadIndicador other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.TipoEducacionCodigo.Equals(TipoEducacionCodigo) && other.AnoNumero.Equals(AnoNumero) && other.GradoCodigo.Equals(GradoCodigo) && other.SectorId.Equals(SectorId) && other.UnidadId.Equals(UnidadId) && other.EjeId.Equals(EjeId) && other.ObjetivoAprendizajeId.Equals(ObjetivoAprendizajeId) && other.IndicadorId.Equals(IndicadorId);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(UnidadIndicador)) return false;
			return Equals((UnidadIndicador)obj);
		}

		public override int GetHashCode()
		{
			return this.TipoEducacionCodigo.GetHashCode() ^ this.AnoNumero.GetHashCode() ^ this.GradoCodigo.GetHashCode() ^ this.SectorId.GetHashCode() ^ this.UnidadId.GetHashCode() ^ this.EjeId.GetHashCode() ^ this.ObjetivoAprendizajeId.GetHashCode() ^ this.IndicadorId.GetHashCode();
		}
	}
}