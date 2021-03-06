using System;
using System.Data.Linq;
namespace RecursoCurricular
{
	[Serializable]
	public partial class Sincronizacion : IEquatable<Sincronizacion>
	{
		public Sincronizacion()
		{
			this.Id = Guid.NewGuid();
			this.estadoSincronizacion = default(EntityRef<RecursoCurricular.EstadoSincronizacion>);
			this.sistema = default(EntityRef<RecursoCurricular.Sistema>);
		}

		public Guid Id { get; set; }

		public Guid SistemaId { get; set; }

		public Int32 EstadoSincronizacionCodigo { get; set; }

		public String Tipo { get; set; }

		public String Objeto { get; set; }

		public String Detalle { get; set; }

		[NonSerialized]
		private EntityRef<RecursoCurricular.EstadoSincronizacion> estadoSincronizacion;
		public RecursoCurricular.EstadoSincronizacion EstadoSincronizacion
		{
			get { return this.estadoSincronizacion.Entity; }
			set { this.estadoSincronizacion.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<RecursoCurricular.Sistema> sistema;
		public RecursoCurricular.Sistema Sistema
		{
			get { return this.sistema.Entity; }
			set { this.sistema.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.Sincronizaciones.Attach(this);
		}

		public bool Equals(Sincronizacion other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.Id.Equals(Id);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(Sincronizacion)) return false;
			return Equals((Sincronizacion)obj);
		}

		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}
	}
}