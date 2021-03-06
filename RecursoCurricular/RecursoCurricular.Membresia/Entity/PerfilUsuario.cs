using System;
using System.Data.Linq;
namespace RecursoCurricular.Membresia
{
	[Serializable]
	public partial class PerfilUsuario : IEquatable<PerfilUsuario>
	{
		public PerfilUsuario()
		{
			this.perfil = default(EntityRef<RecursoCurricular.Membresia.Perfil>);
			this.usuario = default(EntityRef<RecursoCurricular.Membresia.Usuario>);
		}

		public Int32 PerfilCodigo { get; set; }

		public Guid UsuarioId { get; set; }

		public String Url { get; set; }

		public String Valor { get; set; }

		[NonSerialized]
		private EntityRef<RecursoCurricular.Membresia.Perfil> perfil;
		public RecursoCurricular.Membresia.Perfil Perfil
		{
			get { return this.perfil.Entity; }
			set { this.perfil.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<RecursoCurricular.Membresia.Usuario> usuario;
		public RecursoCurricular.Membresia.Usuario Usuario
		{
			get { return this.usuario.Entity; }
			set { this.usuario.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.PerfilUsuarios.Attach(this);
		}

		public bool Equals(PerfilUsuario other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.PerfilCodigo.Equals(PerfilCodigo) && other.UsuarioId.Equals(UsuarioId);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(PerfilUsuario)) return false;
			return Equals((PerfilUsuario)obj);
		}

		public override int GetHashCode()
		{
			return this.PerfilCodigo.GetHashCode() ^ this.UsuarioId.GetHashCode();
		}
	}
}