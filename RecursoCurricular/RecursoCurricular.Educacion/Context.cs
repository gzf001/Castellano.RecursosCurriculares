using System;
using System.Configuration;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using System.Web;
using System.Web.Hosting;
using FluentLinqToSql;
namespace RecursoCurricular.Educacion
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
					if (HttpContext.Current.Items["Educacion_Context"] == null)
					{
						HttpContext.Current.Items["Educacion_Context"] = new Context();
					}

					return (Context)HttpContext.Current.Items["Educacion_Context"];
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
				.AddFromAssemblyContaining<RecursoCurricular.Educacion.Context>()
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

			if (HostingEnvironment.IsHosted) HttpContext.Current.Items["Educacion_Context"] = null;
			else Context.instancia = null;
		}

		#region Tables
		public Table<RecursoCurricular.Educacion.Ciclo> Ciclos
		{
			get { return this.GetTable<RecursoCurricular.Educacion.Ciclo>(); }
		}

		public Table<RecursoCurricular.Educacion.Grado> Grados
		{
			get { return this.GetTable<RecursoCurricular.Educacion.Grado>(); }
		}

		public Table<RecursoCurricular.Educacion.Sector> Sectores
		{
			get { return this.GetTable<RecursoCurricular.Educacion.Sector>(); }
		}

		public Table<RecursoCurricular.Educacion.TipoEducacion> TipoEducaciones
		{
			get { return this.GetTable<RecursoCurricular.Educacion.TipoEducacion>(); }
		}
		#endregion

		#region Views
		#endregion

		#region StoredProcedures
		#endregion

		#region Functions
		#endregion

		#region Configuracion
		public class CicloConfiguration : Mapping<RecursoCurricular.Educacion.Ciclo>
		{
			public CicloConfiguration()
			{
				this.Named("Educacion.Ciclo");

				this.Map<Int32>(x => x.Codigo).Named("Codigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();

			}
		}

		public class GradoConfiguration : Mapping<RecursoCurricular.Educacion.Grado>
		{
			public GradoConfiguration()
			{
				this.Named("Educacion.Grado");

				this.Map<Int32>(x => x.TipoEducacionCodigo).Named("TipoEducacionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.Codigo).Named("Codigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Nullable<Int32>>(x => x.SiguienteTipoEducacionCodigo).Named("SiguienteTipoEducacionCodigo").UpdateCheck(UpdateCheck.Never);
				this.Map<Nullable<Int32>>(x => x.SiguienteGradoCodigo).Named("SiguienteGradoCodigo").UpdateCheck(UpdateCheck.Never);

				this.HasOne<RecursoCurricular.Educacion.Grado>(x => x.SiguienteGrado).ThisKey("SiguienteTipoEducacionCodigo,SiguienteGradoCodigo").OtherKey("TipoEducacionCodigo,Codigo").Storage("siguienteGrado");
				this.HasOne<RecursoCurricular.Educacion.TipoEducacion>(x => x.SiguienteTipoEducacion).ThisKey("SiguienteTipoEducacionCodigo").OtherKey("Codigo").Storage("siguienteTipoEducacion");
				this.HasOne<RecursoCurricular.Educacion.TipoEducacion>(x => x.TipoEducacion).ThisKey("TipoEducacionCodigo").OtherKey("Codigo").Storage("tipoEducacion");
			}
		}

		public class SectorConfiguration : Mapping<RecursoCurricular.Educacion.Sector>
		{
			public SectorConfiguration()
			{
				this.Named("Educacion.Sector");

				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Int32>(x => x.Orden).Named("Orden").UpdateCheck(UpdateCheck.Never).NotNull();

			}
		}

		public class TipoEducacionConfiguration : Mapping<RecursoCurricular.Educacion.TipoEducacion>
		{
			public TipoEducacionConfiguration()
			{
				this.Named("Educacion.TipoEducacion");

				this.Map<Int32>(x => x.Codigo).Named("Codigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();

			}
		}
		#endregion
	}
}