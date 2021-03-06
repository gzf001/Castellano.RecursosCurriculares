using System;
using System.Configuration;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using System.Web;
using System.Web.Hosting;
using FluentLinqToSql;
namespace RecursoCurricular.Membresia
{
	public partial class Context : RecursoCurricular.Context
	{
		private static readonly MappingSource mappingSource;
		private static Context instancia;

		#region Singleton
		public new static Context Instancia
		{
			get
			{
				if (HostingEnvironment.IsHosted)
				{
					if (HttpContext.Current.Items["Membresia_Context"] == null)
					{
						HttpContext.Current.Items["Membresia_Context"] = new Context();
					}

					return (Context)HttpContext.Current.Items["Membresia_Context"];
				}
				else
				{
					if (instancia == null) instancia = new Context { DeferredLoadingEnabled = true };

					return instancia;
				}
			}
		}
		#endregion

		static Context()
		{
			mappingSource = new FluentMappingSource("RecursoCurricular")
				.AddFromAssemblyContaining<RecursoCurricular.Context>()
				.AddFromAssemblyContaining<RecursoCurricular.Membresia.Context>()
				.CreateMappingSource();

			instancia = new Context
			{
				DeferredLoadingEnabled = true
			};
		}

		public Context() : base(mappingSource) { }

		public Context(MappingSource mappingSource) : base(mappingSource) { }

		public override void SubmitChanges(ConflictMode failureMode)
		{
			base.SubmitChanges(failureMode);

			if (HostingEnvironment.IsHosted) HttpContext.Current.Items["Membresia_Context"] = null;
			else Context.instancia = null;
		}

		#region Tables
		public Table<RecursoCurricular.Membresia.Accion> Acciones
		{
			get { return this.GetTable<RecursoCurricular.Membresia.Accion>(); }
		}

		public Table<RecursoCurricular.Membresia.Aplicacion> Aplicaciones
		{
			get { return this.GetTable<RecursoCurricular.Membresia.Aplicacion>(); }
		}

		public Table<RecursoCurricular.Membresia.AplicacionPerfil> AplicacionPerfiles
		{
			get { return this.GetTable<RecursoCurricular.Membresia.AplicacionPerfil>(); }
		}

		public Table<RecursoCurricular.Membresia.Auditoria> Auditorias
		{
			get { return this.GetTable<RecursoCurricular.Membresia.Auditoria>(); }
		}

		public Table<RecursoCurricular.Membresia.MenuItem> MenuItemes
		{
			get { return this.GetTable<RecursoCurricular.Membresia.MenuItem>(); }
		}

		public Table<RecursoCurricular.Membresia.MenuItemAccion> MenuItemAcciones
		{
			get { return this.GetTable<RecursoCurricular.Membresia.MenuItemAccion>(); }
		}

		public Table<RecursoCurricular.Membresia.Perfil> Perfiles
		{
			get { return this.GetTable<RecursoCurricular.Membresia.Perfil>(); }
		}

		public Table<RecursoCurricular.Membresia.PerfilUsuario> PerfilUsuarios
		{
			get { return this.GetTable<RecursoCurricular.Membresia.PerfilUsuario>(); }
		}

		public Table<RecursoCurricular.Membresia.Rol> Roles
		{
			get { return this.GetTable<RecursoCurricular.Membresia.Rol>(); }
		}

		public Table<RecursoCurricular.Membresia.RolAccion> RolAcciones
		{
			get { return this.GetTable<RecursoCurricular.Membresia.RolAccion>(); }
		}

		public Table<RecursoCurricular.Membresia.RolPersona> RolPersonas
		{
			get { return this.GetTable<RecursoCurricular.Membresia.RolPersona>(); }
		}

		public Table<RecursoCurricular.Membresia.Usuario> Usuarios
		{
			get { return this.GetTable<RecursoCurricular.Membresia.Usuario>(); }
		}
		#endregion

		#region Views
		#endregion

		#region StoredProcedures
		#endregion

		#region Functions
		#endregion

		#region Configuracion
		public class AccionConfiguration : Mapping<RecursoCurricular.Membresia.Accion>
		{
			public AccionConfiguration()
			{
				this.Named("Membresia.Accion");

				this.Map<Int32>(x => x.Codigo).Named("Codigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();

			}
		}

		public class AplicacionConfiguration : Mapping<RecursoCurricular.Membresia.Aplicacion>
		{
			public AplicacionConfiguration()
			{
				this.Named("Membresia.Aplicacion");

				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Nullable<Guid>>(x => x.MenuItemId).Named("MenuItemId").UpdateCheck(UpdateCheck.Never);
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Clave).Named("Clave").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Fa_Icon).Named("Fa_Icon").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Byte>(x => x.Orden).Named("Orden").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<RecursoCurricular.Membresia.MenuItem>(x => x.Inicio).ThisKey("Id,MenuItemId").OtherKey("AplicacionId,Id").Storage("inicio");
			}
		}

		public class AplicacionPerfilConfiguration : Mapping<RecursoCurricular.Membresia.AplicacionPerfil>
		{
			public AplicacionPerfilConfiguration()
			{
				this.Named("Membresia.AplicacionPerfil");

				this.Map<Guid>(x => x.AplicacionId).Named("AplicacionId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.PerfilCodigo).Named("PerfilCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();

				this.HasOne<RecursoCurricular.Membresia.Aplicacion>(x => x.Aplicacion).ThisKey("AplicacionId").OtherKey("Id").Storage("aplicacion");
				this.HasOne<RecursoCurricular.Membresia.Perfil>(x => x.Perfil).ThisKey("PerfilCodigo").OtherKey("Codigo").Storage("perfil");
			}
		}

		public class AuditoriaConfiguration : Mapping<RecursoCurricular.Membresia.Auditoria>
		{
			public AuditoriaConfiguration()
			{
				this.Named("Membresia.Auditoria");

				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.UsuarioId).Named("UsuarioId").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Guid>(x => x.AplicacionId).Named("AplicacionId").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Guid>(x => x.MenuItemId).Named("MenuItemId").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Actividad).Named("Actividad").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<DateTime>(x => x.Fecha).Named("Fecha").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<RecursoCurricular.Membresia.Aplicacion>(x => x.Aplicacion).ThisKey("AplicacionId").OtherKey("Id").Storage("aplicacion");
				this.HasOne<RecursoCurricular.Membresia.MenuItem>(x => x.MenuItem).ThisKey("AplicacionId,MenuItemId").OtherKey("AplicacionId,Id").Storage("menuItem");
				this.HasOne<RecursoCurricular.Membresia.Usuario>(x => x.Usuario).ThisKey("UsuarioId").OtherKey("Id").Storage("usuario");
			}
		}

		public class MenuItemConfiguration : Mapping<RecursoCurricular.Membresia.MenuItem>
		{
			public MenuItemConfiguration()
			{
				this.Named("Membresia.MenuItem");

				this.Map<Guid>(x => x.AplicacionId).Named("AplicacionId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Nullable<Guid>>(x => x.MenuItemId).Named("MenuItemId").UpdateCheck(UpdateCheck.Never);
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Informacion).Named("Informacion").UpdateCheck(UpdateCheck.Never);
				this.Map<String>(x => x.Icono).Named("Icono").UpdateCheck(UpdateCheck.Never);
				this.Map<String>(x => x.Url).Named("Url").UpdateCheck(UpdateCheck.Never);
				this.Map<Boolean>(x => x.Visible).Named("Visible").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Int32>(x => x.Orden).Named("Orden").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<RecursoCurricular.Membresia.Aplicacion>(x => x.Aplicacion).ThisKey("AplicacionId").OtherKey("Id").Storage("aplicacion");
				this.HasOne<RecursoCurricular.Membresia.MenuItem>(x => x.Padre).ThisKey("AplicacionId,MenuItemId").OtherKey("AplicacionId,Id").Storage("padre");
			}
		}

		public class MenuItemAccionConfiguration : Mapping<RecursoCurricular.Membresia.MenuItemAccion>
		{
			public MenuItemAccionConfiguration()
			{
				this.Named("Membresia.MenuItemAccion");

				this.Map<Guid>(x => x.AplicacionId).Named("AplicacionId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.MenuItemId).Named("MenuItemId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.AccionCodigo).Named("AccionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();

				this.HasOne<RecursoCurricular.Membresia.Accion>(x => x.Accion).ThisKey("AccionCodigo").OtherKey("Codigo").Storage("accion");
				this.HasOne<RecursoCurricular.Membresia.MenuItem>(x => x.MenuItem).ThisKey("AplicacionId,MenuItemId").OtherKey("AplicacionId,Id").Storage("menuItem");
			}
		}

		public class PerfilConfiguration : Mapping<RecursoCurricular.Membresia.Perfil>
		{
			public PerfilConfiguration()
			{
				this.Named("Membresia.Perfil");

				this.Map<Int32>(x => x.Codigo).Named("Codigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Clave).Named("Clave").UpdateCheck(UpdateCheck.Never).NotNull();

			}
		}

		public class PerfilUsuarioConfiguration : Mapping<RecursoCurricular.Membresia.PerfilUsuario>
		{
			public PerfilUsuarioConfiguration()
			{
				this.Named("Membresia.PerfilUsuario");

				this.Map<Int32>(x => x.PerfilCodigo).Named("PerfilCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.UsuarioId).Named("UsuarioId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Url).Named("Url").UpdateCheck(UpdateCheck.Never);
				this.Map<String>(x => x.Valor).Named("Valor").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<RecursoCurricular.Membresia.Perfil>(x => x.Perfil).ThisKey("PerfilCodigo").OtherKey("Codigo").Storage("perfil");
				this.HasOne<RecursoCurricular.Membresia.Usuario>(x => x.Usuario).ThisKey("UsuarioId").OtherKey("Id").Storage("usuario");
			}
		}

		public class RolConfiguration : Mapping<RecursoCurricular.Membresia.Rol>
		{
			public RolConfiguration()
			{
				this.Named("Membresia.Rol");

				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Clave).Named("Clave").UpdateCheck(UpdateCheck.Never);

			}
		}

		public class RolAccionConfiguration : Mapping<RecursoCurricular.Membresia.RolAccion>
		{
			public RolAccionConfiguration()
			{
				this.Named("Membresia.RolAccion");

				this.Map<Guid>(x => x.RolId).Named("RolId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.AplicacionId).Named("AplicacionId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.MenuItemId).Named("MenuItemId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.AccionCodigo).Named("AccionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();

				this.HasOne<RecursoCurricular.Membresia.Accion>(x => x.Accion).ThisKey("AccionCodigo").OtherKey("Codigo").Storage("accion");
				this.HasOne<RecursoCurricular.Membresia.MenuItem>(x => x.MenuItem).ThisKey("AplicacionId,MenuItemId").OtherKey("AplicacionId,Id").Storage("menuItem");
				this.HasOne<RecursoCurricular.Membresia.MenuItemAccion>(x => x.MenuItemAccion).ThisKey("AplicacionId,MenuItemId,AccionCodigo").OtherKey("AplicacionId,MenuItemId,AccionCodigo").Storage("menuItemAccion");
				this.HasOne<RecursoCurricular.Membresia.Rol>(x => x.Rol).ThisKey("RolId").OtherKey("Id").Storage("rol");
			}
		}

		public class RolPersonaConfiguration : Mapping<RecursoCurricular.Membresia.RolPersona>
		{
			public RolPersonaConfiguration()
			{
				this.Named("Membresia.RolPersona");

				this.Map<Guid>(x => x.RolId).Named("RolId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.PersonaId).Named("PersonaId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();

				this.HasOne<RecursoCurricular.Persona>(x => x.Persona).ThisKey("PersonaId").OtherKey("Id").Storage("persona");
				this.HasOne<RecursoCurricular.Membresia.Rol>(x => x.Rol).ThisKey("RolId").OtherKey("Id").Storage("rol");
			}
		}

		public class UsuarioConfiguration : Mapping<RecursoCurricular.Membresia.Usuario>
		{
			public UsuarioConfiguration()
			{
				this.Named("Membresia.Usuario");

				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Password).Named("Password").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Boolean>(x => x.Aprobado).Named("Aprobado").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Boolean>(x => x.Bloqueado).Named("Bloqueado").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<DateTime>(x => x.Creacion).Named("Creacion").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<DateTime>(x => x.UltimaActividad).Named("UltimaActividad").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<DateTime>(x => x.UltimoAcceso).Named("UltimoAcceso").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Nullable<DateTime>>(x => x.UltimoCambioPassword).Named("UltimoCambioPassword").UpdateCheck(UpdateCheck.Never);
				this.Map<Nullable<DateTime>>(x => x.UltimoDesbloqueo).Named("UltimoDesbloqueo").UpdateCheck(UpdateCheck.Never);
				this.Map<Int16>(x => x.NumeroIntentosFallidos).Named("NumeroIntentosFallidos").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Nullable<DateTime>>(x => x.FechaIntentoFallido).Named("FechaIntentoFallido").UpdateCheck(UpdateCheck.Never);

				this.HasOne<RecursoCurricular.Persona>(x => x.Persona).ThisKey("Id").OtherKey("Id").Storage("persona");
			}
		}
		#endregion
	}
}