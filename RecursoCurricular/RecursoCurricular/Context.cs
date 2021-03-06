using System;
using System.Configuration;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using System.Web;
using System.Web.Hosting;
using FluentLinqToSql;
namespace RecursoCurricular
{
	public partial class Context : DataContext
	{
		protected static readonly string connectionString = ConfigurationManager.ConnectionStrings["RecursoCurricular"].ConnectionString;
		private static readonly MappingSource mappingSource;
		private static Context instancia;

		#region Singleton
		public static Context Instancia
		{
			get
			{
				if (HostingEnvironment.IsHosted)
				{
					if (HttpContext.Current.Items["RecursoCurricular_Context"] == null)
					{
						HttpContext.Current.Items["RecursoCurricular_Context"] = new Context();
					}

					return (Context)HttpContext.Current.Items["RecursoCurricular_Context"];
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
				.CreateMappingSource();

			instancia = new Context
			{
				DeferredLoadingEnabled = true
			};
		}

		public Context() : base(connectionString, mappingSource) { }

		public Context(MappingSource mappingSource) : base(connectionString, mappingSource) { }

		public override void SubmitChanges(ConflictMode failureMode)
		{
			base.SubmitChanges(failureMode);

			if (HostingEnvironment.IsHosted) HttpContext.Current.Items["RecursoCurricular_Context"] = null;
			else Context.instancia = null;
		}

		#region Tables
		public Table<RecursoCurricular.Anio> Anios
		{
			get { return this.GetTable<RecursoCurricular.Anio>(); }
		}

		public Table<RecursoCurricular.Ciudad> Ciudades
		{
			get { return this.GetTable<RecursoCurricular.Ciudad>(); }
		}

		public Table<RecursoCurricular.Comuna> Comunas
		{
			get { return this.GetTable<RecursoCurricular.Comuna>(); }
		}

		public Table<RecursoCurricular.DimensionHabilidadTic> DimensionHabilidadTices
		{
			get { return this.GetTable<RecursoCurricular.DimensionHabilidadTic>(); }
		}

		public Table<RecursoCurricular.EstadoCivil> EstadoCiviles
		{
			get { return this.GetTable<RecursoCurricular.EstadoCivil>(); }
		}

		public Table<RecursoCurricular.EstadoSincronizacion> EstadoSincronizaciones
		{
			get { return this.GetTable<RecursoCurricular.EstadoSincronizacion>(); }
		}

		public Table<RecursoCurricular.HabilidadTic> HabilidadTices
		{
			get { return this.GetTable<RecursoCurricular.HabilidadTic>(); }
		}

		public Table<RecursoCurricular.Nacionalidad> Nacionalidades
		{
			get { return this.GetTable<RecursoCurricular.Nacionalidad>(); }
		}

		public Table<RecursoCurricular.NivelEducacional> NivelEducacionales
		{
			get { return this.GetTable<RecursoCurricular.NivelEducacional>(); }
		}

		public Table<RecursoCurricular.Persona> Personas
		{
			get { return this.GetTable<RecursoCurricular.Persona>(); }
		}

		public Table<RecursoCurricular.Region> Regiones
		{
			get { return this.GetTable<RecursoCurricular.Region>(); }
		}

		public Table<RecursoCurricular.Sexo> Sexos
		{
			get { return this.GetTable<RecursoCurricular.Sexo>(); }
		}

		public Table<RecursoCurricular.Sincronizacion> Sincronizaciones
		{
			get { return this.GetTable<RecursoCurricular.Sincronizacion>(); }
		}

		public Table<RecursoCurricular.Sistema> Sistemas
		{
			get { return this.GetTable<RecursoCurricular.Sistema>(); }
		}
		#endregion

		#region Views
		#endregion

		#region StoredProcedures
		#endregion

		#region Functions
		public String FormatInt(Int32 Number)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, (MethodInfo)MethodInfo.GetCurrentMethod(), Number);

			return (String)result.ReturnValue;
		}

		public Nullable<Char> GetRunDigito(Int32 RunCuerpo)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, (MethodInfo)MethodInfo.GetCurrentMethod(), RunCuerpo);

			return (Nullable<Char>)result.ReturnValue;
		}
		#endregion

		#region Configuracion
		public class AnioConfiguration : Mapping<RecursoCurricular.Anio>
		{
			public AnioConfiguration()
			{
				this.Named("dbo.Anio");

				this.Map<Int32>(x => x.Numero).Named("Numero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Boolean>(x => x.Activo).Named("Activo").UpdateCheck(UpdateCheck.Never).NotNull();

			}
		}

		public class CiudadConfiguration : Mapping<RecursoCurricular.Ciudad>
		{
			public CiudadConfiguration()
			{
				this.Named("dbo.Ciudad");

				this.Map<Int16>(x => x.RegionCodigo).Named("RegionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int16>(x => x.Codigo).Named("Codigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<RecursoCurricular.Region>(x => x.Region).ThisKey("RegionCodigo").OtherKey("Codigo").Storage("region");
			}
		}

		public class ComunaConfiguration : Mapping<RecursoCurricular.Comuna>
		{
			public ComunaConfiguration()
			{
				this.Named("dbo.Comuna");

				this.Map<Int16>(x => x.RegionCodigo).Named("RegionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int16>(x => x.CiudadCodigo).Named("CiudadCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int16>(x => x.Codigo).Named("Codigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<RecursoCurricular.Ciudad>(x => x.Ciudad).ThisKey("RegionCodigo,CiudadCodigo").OtherKey("RegionCodigo,Codigo").Storage("ciudad");
			}
		}

		public class DimensionHabilidadTicConfiguration : Mapping<RecursoCurricular.DimensionHabilidadTic>
		{
			public DimensionHabilidadTicConfiguration()
			{
				this.Named("dbo.DimensionHabilidadTic");

				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.AnoNumero).Named("AnoNumero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.Numero).Named("Numero").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Descripcion).Named("Descripcion").UpdateCheck(UpdateCheck.Never);

				this.HasOne<RecursoCurricular.Anio>(x => x.Anio).ThisKey("AnoNumero").OtherKey("Numero").Storage("anio");
			}
		}

		public class EstadoCivilConfiguration : Mapping<RecursoCurricular.EstadoCivil>
		{
			public EstadoCivilConfiguration()
			{
				this.Named("dbo.EstadoCivil");

				this.Map<Int16>(x => x.Codigo).Named("Codigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();

			}
		}

		public class EstadoSincronizacionConfiguration : Mapping<RecursoCurricular.EstadoSincronizacion>
		{
			public EstadoSincronizacionConfiguration()
			{
				this.Named("dbo.EstadoSincronizacion");

				this.Map<Int32>(x => x.Codigo).Named("Codigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();

			}
		}

		public class HabilidadTicConfiguration : Mapping<RecursoCurricular.HabilidadTic>
		{
			public HabilidadTicConfiguration()
			{
				this.Named("dbo.HabilidadTic");

				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.DimensionHabilidadTicId).Named("DimensionHabilidadTicId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.AnoNumero).Named("AnoNumero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.Numero).Named("Numero").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Descripcion).Named("Descripcion").UpdateCheck(UpdateCheck.Never);

				this.HasOne<RecursoCurricular.Anio>(x => x.Anio).ThisKey("AnoNumero").OtherKey("Numero").Storage("anio");
				this.HasOne<RecursoCurricular.DimensionHabilidadTic>(x => x.DimensionHabilidadTIC).ThisKey("DimensionHabilidadTicId,AnoNumero").OtherKey("Id,AnoNumero").Storage("dimensionHabilidadTIC");
			}
		}

		public class NacionalidadConfiguration : Mapping<RecursoCurricular.Nacionalidad>
		{
			public NacionalidadConfiguration()
			{
				this.Named("dbo.Nacionalidad");

				this.Map<Int16>(x => x.Codigo).Named("Codigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Boolean>(x => x.Extranjero).Named("Extranjero").UpdateCheck(UpdateCheck.Never).NotNull();

			}
		}

		public class NivelEducacionalConfiguration : Mapping<RecursoCurricular.NivelEducacional>
		{
			public NivelEducacionalConfiguration()
			{
				this.Named("dbo.NivelEducacional");

				this.Map<Int32>(x => x.Codigo).Named("Codigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();

			}
		}

		public class PersonaConfiguration : Mapping<RecursoCurricular.Persona>
		{
			public PersonaConfiguration()
			{
				this.Named("dbo.Persona");

				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Run).Named("Run").UpdateCheck(UpdateCheck.Never).DbGenerated();
				this.Map<Int32>(x => x.RunCuerpo).Named("RunCuerpo").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Char>(x => x.RunDigito).Named("RunDigito").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).DbGenerated();
				this.Map<String>(x => x.Nombres).Named("Nombres").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.ApellidoPaterno).Named("ApellidoPaterno").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.ApellidoMaterno).Named("ApellidoMaterno").UpdateCheck(UpdateCheck.Never);
				this.Map<String>(x => x.Email).Named("Email").UpdateCheck(UpdateCheck.Never);
				this.Map<Int16>(x => x.SexoCodigo).Named("SexoCodigo").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Nullable<DateTime>>(x => x.FechaNacimiento).Named("FechaNacimiento").UpdateCheck(UpdateCheck.Never);
				this.Map<Nullable<Int16>>(x => x.NacionalidadCodigo).Named("NacionalidadCodigo").UpdateCheck(UpdateCheck.Never);
				this.Map<Nullable<Int16>>(x => x.EstadoCivilCodigo).Named("EstadoCivilCodigo").UpdateCheck(UpdateCheck.Never);
				this.Map<Nullable<Int32>>(x => x.NivelEducacionalCodigo).Named("NivelEducacionalCodigo").UpdateCheck(UpdateCheck.Never);
				this.Map<Nullable<Int16>>(x => x.RegionCodigo).Named("RegionCodigo").UpdateCheck(UpdateCheck.Never);
				this.Map<Nullable<Int16>>(x => x.CiudadCodigo).Named("CiudadCodigo").UpdateCheck(UpdateCheck.Never);
				this.Map<Nullable<Int16>>(x => x.ComunaCodigo).Named("ComunaCodigo").UpdateCheck(UpdateCheck.Never);
				this.Map<String>(x => x.VillaPoblacion).Named("VillaPoblacion").UpdateCheck(UpdateCheck.Never);
				this.Map<String>(x => x.Direccion).Named("Direccion").UpdateCheck(UpdateCheck.Never);
				this.Map<Nullable<Int32>>(x => x.Telefono).Named("Telefono").UpdateCheck(UpdateCheck.Never);
				this.Map<Nullable<Int32>>(x => x.Celular).Named("Celular").UpdateCheck(UpdateCheck.Never);
				this.Map<String>(x => x.Observaciones).Named("Observaciones").UpdateCheck(UpdateCheck.Never);
				this.Map<String>(x => x.Ocupacion).Named("Ocupacion").UpdateCheck(UpdateCheck.Never);
				this.Map<Nullable<Int32>>(x => x.TelefonoLaboral).Named("TelefonoLaboral").UpdateCheck(UpdateCheck.Never);
				this.Map<String>(x => x.DireccionLaboral).Named("DireccionLaboral").UpdateCheck(UpdateCheck.Never);

				this.HasOne<RecursoCurricular.Comuna>(x => x.Comuna).ThisKey("RegionCodigo,CiudadCodigo,ComunaCodigo").OtherKey("RegionCodigo,CiudadCodigo,Codigo").Storage("comuna");
				this.HasOne<RecursoCurricular.EstadoCivil>(x => x.EstadoCivil).ThisKey("EstadoCivilCodigo").OtherKey("Codigo").Storage("estadoCivil");
				this.HasOne<RecursoCurricular.Nacionalidad>(x => x.Nacionalidad).ThisKey("NacionalidadCodigo").OtherKey("Codigo").Storage("nacionalidad");
				this.HasOne<RecursoCurricular.NivelEducacional>(x => x.NivelEducacional).ThisKey("NivelEducacionalCodigo").OtherKey("Codigo").Storage("nivelEducacional");
				this.HasOne<RecursoCurricular.Sexo>(x => x.Sexo).ThisKey("SexoCodigo").OtherKey("Codigo").Storage("sexo");
			}
		}

		public class RegionConfiguration : Mapping<RecursoCurricular.Region>
		{
			public RegionConfiguration()
			{
				this.Named("dbo.Region");

				this.Map<Int16>(x => x.Codigo).Named("Codigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();

			}
		}

		public class SexoConfiguration : Mapping<RecursoCurricular.Sexo>
		{
			public SexoConfiguration()
			{
				this.Named("dbo.Sexo");

				this.Map<Int16>(x => x.Codigo).Named("Codigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Char>(x => x.Letra).Named("Letra").UpdateCheck(UpdateCheck.Never).NotNull();

			}
		}

		public class SincronizacionConfiguration : Mapping<RecursoCurricular.Sincronizacion>
		{
			public SincronizacionConfiguration()
			{
				this.Named("dbo.Sincronizacion");

				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.SistemaId).Named("SistemaId").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Int32>(x => x.EstadoSincronizacionCodigo).Named("EstadoSincronizacionCodigo").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Tipo).Named("Tipo").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Objeto).Named("Objeto").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Detalle).Named("Detalle").UpdateCheck(UpdateCheck.Never);

				this.HasOne<RecursoCurricular.EstadoSincronizacion>(x => x.EstadoSincronizacion).ThisKey("EstadoSincronizacionCodigo").OtherKey("Codigo").Storage("estadoSincronizacion");
				this.HasOne<RecursoCurricular.Sistema>(x => x.Sistema).ThisKey("SistemaId").OtherKey("Id").Storage("sistema");
			}
		}

		public class SistemaConfiguration : Mapping<RecursoCurricular.Sistema>
		{
			public SistemaConfiguration()
			{
				this.Named("dbo.Sistema");

				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Url).Named("Url").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Boolean>(x => x.Activo).Named("Activo").UpdateCheck(UpdateCheck.Never).NotNull();

			}
		}

		public class FunctionMapping : FunctionMapping<Context>
		{
			public FunctionMapping()
			{
				this.Map(x => x.FormatInt(
					Parameter<Int32>(param => param.Named("Number"))
					)).Named("dbo.FormatInt").Composable();

				this.Map(x => x.GetRunDigito(
					Parameter<Int32>(param => param.Named("RunCuerpo"))
					)).Named("dbo.GetRunDigito").Composable();
			}
		}
		#endregion
	}
}