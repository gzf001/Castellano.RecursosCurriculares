using System;
using System.Data.Linq;
namespace RecursoCurricular.RecursosCurriculares
{
	[Serializable]
	public partial class AprendizajeIndicador : IEquatable<AprendizajeIndicador>
	{
		public AprendizajeIndicador()
		{
			this.Id = Guid.NewGuid();
			this.anio = default(EntityRef<RecursoCurricular.Anio>);
			this.aprendizaje = default(EntityRef<RecursoCurricular.RecursosCurriculares.Aprendizaje>);
		}

		public Int32 AnoNumero { get; set; }

		public Int32 TipoEducacionCodigo { get; set; }

		public Int32 GradoCodigo { get; set; }

		public Guid SectorId { get; set; }

		public Guid AprendizajeId { get; set; }

		public Guid Id { get; set; }

		public Nullable<Int32> CategoriaCodigo { get; set; }

		public Int32 Numero { get; set; }

		public String Descripcion { get; set; }

		[NonSerialized]
		private EntityRef<RecursoCurricular.Anio> anio;
		public RecursoCurricular.Anio Anio
		{
			get { return this.anio.Entity; }
			set { this.anio.Entity = value; }
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
			Context.Instancia.AprendizajeIndicadores.Attach(this);
		}

		public bool Equals(AprendizajeIndicador other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.AnoNumero.Equals(AnoNumero) && other.TipoEducacionCodigo.Equals(TipoEducacionCodigo) && other.GradoCodigo.Equals(GradoCodigo) && other.SectorId.Equals(SectorId) && other.AprendizajeId.Equals(AprendizajeId) && other.Id.Equals(Id);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(AprendizajeIndicador)) return false;
			return Equals((AprendizajeIndicador)obj);
		}

		public override int GetHashCode()
		{
			return this.AnoNumero.GetHashCode() ^ this.TipoEducacionCodigo.GetHashCode() ^ this.GradoCodigo.GetHashCode() ^ this.SectorId.GetHashCode() ^ this.AprendizajeId.GetHashCode() ^ this.Id.GetHashCode();
		}
	}
}