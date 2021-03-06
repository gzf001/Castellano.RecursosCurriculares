using System;
using System.Data.Linq;
namespace RecursoCurricular.RecursosCurriculares
{
	[Serializable]
	public partial class TipoEducacionEje : IEquatable<TipoEducacionEje>
	{
		public TipoEducacionEje()
		{
			this.eje = default(EntityRef<RecursoCurricular.RecursosCurriculares.Eje>);
			this.tipoEducacion = default(EntityRef<RecursoCurricular.Educacion.TipoEducacion>);
		}

		public Int32 TipoEducacionCodigo { get; set; }

		public Int32 AnoNumero { get; set; }

		public Guid SectorId { get; set; }

		public Guid EjeId { get; set; }

		[NonSerialized]
		private EntityRef<RecursoCurricular.RecursosCurriculares.Eje> eje;
		public RecursoCurricular.RecursosCurriculares.Eje Eje
		{
			get { return this.eje.Entity; }
			set { this.eje.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<RecursoCurricular.Educacion.TipoEducacion> tipoEducacion;
		public RecursoCurricular.Educacion.TipoEducacion TipoEducacion
		{
			get { return this.tipoEducacion.Entity; }
			set { this.tipoEducacion.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.TipoEducacionEjes.Attach(this);
		}

		public bool Equals(TipoEducacionEje other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.TipoEducacionCodigo.Equals(TipoEducacionCodigo) && other.AnoNumero.Equals(AnoNumero) && other.SectorId.Equals(SectorId) && other.EjeId.Equals(EjeId);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(TipoEducacionEje)) return false;
			return Equals((TipoEducacionEje)obj);
		}

		public override int GetHashCode()
		{
			return this.TipoEducacionCodigo.GetHashCode() ^ this.AnoNumero.GetHashCode() ^ this.SectorId.GetHashCode() ^ this.EjeId.GetHashCode();
		}
	}
}