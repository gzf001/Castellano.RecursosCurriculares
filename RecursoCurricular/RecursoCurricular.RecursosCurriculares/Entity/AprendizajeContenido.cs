using System;
using System.Data.Linq;
namespace RecursoCurricular.RecursosCurriculares
{
	[Serializable]
	public partial class AprendizajeContenido : IEquatable<AprendizajeContenido>
	{
		public AprendizajeContenido()
		{
			this.ano = default(EntityRef<RecursoCurricular.Ano>);
			this.aprendizaje = default(EntityRef<RecursoCurricular.RecursosCurriculares.Aprendizaje>);
		}

		public Int32 AnoNumero { get; set; }

		public Int32 TipoEducacionCodigo { get; set; }

		public Int32 GradoCodigo { get; set; }

		public Guid SectorId { get; set; }

		public Guid AprendizajeId { get; set; }

		public Guid EjeId { get; set; }

		public Guid ContenidoId { get; set; }

		[NonSerialized]
		private EntityRef<RecursoCurricular.Ano> ano;
		public RecursoCurricular.Ano Ano
		{
			get { return this.ano.Entity; }
			set { this.ano.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<RecursoCurricular.RecursosCurriculares.Aprendizaje> aprendizaje;
		public RecursoCurricular.RecursosCurriculares.Aprendizaje Aprendizaje
		{
			get { return this.aprendizaje.Entity; }
			set { this.aprendizaje.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.AprendizajeContenidos.Attach(this);
		}

		public bool Equals(AprendizajeContenido other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.AnoNumero.Equals(AnoNumero) && other.TipoEducacionCodigo.Equals(TipoEducacionCodigo) && other.GradoCodigo.Equals(GradoCodigo) && other.SectorId.Equals(SectorId) && other.AprendizajeId.Equals(AprendizajeId) && other.EjeId.Equals(EjeId) && other.ContenidoId.Equals(ContenidoId);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(AprendizajeContenido)) return false;
			return Equals((AprendizajeContenido)obj);
		}

		public override int GetHashCode()
		{
			return this.AnoNumero.GetHashCode() ^ this.TipoEducacionCodigo.GetHashCode() ^ this.GradoCodigo.GetHashCode() ^ this.SectorId.GetHashCode() ^ this.AprendizajeId.GetHashCode() ^ this.EjeId.GetHashCode() ^ this.ContenidoId.GetHashCode();
		}
	}
}