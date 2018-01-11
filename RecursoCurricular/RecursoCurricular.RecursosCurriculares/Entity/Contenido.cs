using System;
using System.Data.Linq;
namespace RecursoCurricular.RecursosCurriculares
{
	[Serializable]
	public partial class Contenido : IEquatable<Contenido>
	{
		public Contenido()
		{
			this.Id = Guid.NewGuid();
			this.anio = default(EntityRef<RecursoCurricular.Anio>);
			this.eje = default(EntityRef<RecursoCurricular.RecursosCurriculares.Eje>);
			this.sector = default(EntityRef<RecursoCurricular.Educacion.Sector>);
		}

		public Int32 AnoNumero { get; set; }

		public Int32 TipoEducacionCodigo { get; set; }

		public Guid SectorId { get; set; }

		public Guid EjeId { get; set; }

		public Int32 GradoCodigo { get; set; }

		public Guid Id { get; set; }

		public Int32 Numero { get; set; }

		public String Descripcion { get; set; }

		public Boolean Transversal { get; set; }

		[NonSerialized]
		private EntityRef<RecursoCurricular.Anio> anio;
		public RecursoCurricular.Anio Anio
		{
			get { return this.anio.Entity; }
			set { this.anio.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<RecursoCurricular.RecursosCurriculares.Eje> eje;
		public RecursoCurricular.RecursosCurriculares.Eje Eje
		{
			get { return this.eje.Entity; }
			set { this.eje.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<RecursoCurricular.Educacion.Sector> sector;
		public RecursoCurricular.Educacion.Sector Sector
		{
			get { return this.sector.Entity; }
			set { this.sector.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.Contenidos.Attach(this);
		}

		public bool Equals(Contenido other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.AnoNumero.Equals(AnoNumero) && other.TipoEducacionCodigo.Equals(TipoEducacionCodigo) && other.SectorId.Equals(SectorId) && other.EjeId.Equals(EjeId) && other.GradoCodigo.Equals(GradoCodigo) && other.Id.Equals(Id);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(Contenido)) return false;
			return Equals((Contenido)obj);
		}

		public override int GetHashCode()
		{
			return this.AnoNumero.GetHashCode() ^ this.TipoEducacionCodigo.GetHashCode() ^ this.SectorId.GetHashCode() ^ this.EjeId.GetHashCode() ^ this.GradoCodigo.GetHashCode() ^ this.Id.GetHashCode();
		}
	}
}