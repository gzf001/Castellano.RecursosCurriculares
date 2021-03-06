using System;
using System.Configuration;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using System.Web;
using System.Web.Hosting;
using FluentLinqToSql;
namespace RecursoCurricular.BaseCurricular
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
					if (HttpContext.Current.Items["BaseCurricular_Context"] == null)
					{
						HttpContext.Current.Items["BaseCurricular_Context"] = new Context();
					}

					return (Context)HttpContext.Current.Items["BaseCurricular_Context"];
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
				.AddFromAssemblyContaining<RecursoCurricular.BaseCurricular.Context>()
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

			if (HostingEnvironment.IsHosted) HttpContext.Current.Items["BaseCurricular_Context"] = null;
			else Context.instancia = null;
		}

		#region Tables
		public Table<RecursoCurricular.BaseCurricular.Actitud> Actitudes
		{
			get { return this.GetTable<RecursoCurricular.BaseCurricular.Actitud>(); }
		}

		public Table<RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje> AmbitoExperienciaAprendizajes
		{
			get { return this.GetTable<RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje>(); }
		}

		public Table<RecursoCurricular.BaseCurricular.AprendizajeEsperadoParvulo> AprendizajeEsperadoParvulos
		{
			get { return this.GetTable<RecursoCurricular.BaseCurricular.AprendizajeEsperadoParvulo>(); }
		}

		public Table<RecursoCurricular.BaseCurricular.Conocimiento> Conocimientos
		{
			get { return this.GetTable<RecursoCurricular.BaseCurricular.Conocimiento>(); }
		}

		public Table<RecursoCurricular.BaseCurricular.DimensionOAT> DimensionOATes
		{
			get { return this.GetTable<RecursoCurricular.BaseCurricular.DimensionOAT>(); }
		}

		public Table<RecursoCurricular.BaseCurricular.Eje> Ejes
		{
			get { return this.GetTable<RecursoCurricular.BaseCurricular.Eje>(); }
		}

		public Table<RecursoCurricular.BaseCurricular.EjeParvulo> EjeParvulos
		{
			get { return this.GetTable<RecursoCurricular.BaseCurricular.EjeParvulo>(); }
		}

		public Table<RecursoCurricular.BaseCurricular.Habilidad> Habilidades
		{
			get { return this.GetTable<RecursoCurricular.BaseCurricular.Habilidad>(); }
		}

		public Table<RecursoCurricular.BaseCurricular.Indicador> Indicadores
		{
			get { return this.GetTable<RecursoCurricular.BaseCurricular.Indicador>(); }
		}

		public Table<RecursoCurricular.BaseCurricular.NucleoAprendizaje> NucleoAprendizajes
		{
			get { return this.GetTable<RecursoCurricular.BaseCurricular.NucleoAprendizaje>(); }
		}

		public Table<RecursoCurricular.BaseCurricular.ObjetivoAprendizaje> ObjetivoAprendizajes
		{
			get { return this.GetTable<RecursoCurricular.BaseCurricular.ObjetivoAprendizaje>(); }
		}

		public Table<RecursoCurricular.BaseCurricular.ObjetivoAprendizajeTransversal> ObjetivoAprendizajeTransversales
		{
			get { return this.GetTable<RecursoCurricular.BaseCurricular.ObjetivoAprendizajeTransversal>(); }
		}

		public Table<RecursoCurricular.BaseCurricular.PrincipioPedagogico> PrincipioPedagogicos
		{
			get { return this.GetTable<RecursoCurricular.BaseCurricular.PrincipioPedagogico>(); }
		}

		public Table<RecursoCurricular.BaseCurricular.SubHabilidad> SubHabilidades
		{
			get { return this.GetTable<RecursoCurricular.BaseCurricular.SubHabilidad>(); }
		}

		public Table<RecursoCurricular.BaseCurricular.TipoEducacionEje> TipoEducacionEjes
		{
			get { return this.GetTable<RecursoCurricular.BaseCurricular.TipoEducacionEje>(); }
		}

		public Table<RecursoCurricular.BaseCurricular.Unidad> Unidades
		{
			get { return this.GetTable<RecursoCurricular.BaseCurricular.Unidad>(); }
		}

		public Table<RecursoCurricular.BaseCurricular.UnidadActitud> UnidadActitudes
		{
			get { return this.GetTable<RecursoCurricular.BaseCurricular.UnidadActitud>(); }
		}

		public Table<RecursoCurricular.BaseCurricular.UnidadConocimiento> UnidadConocimientos
		{
			get { return this.GetTable<RecursoCurricular.BaseCurricular.UnidadConocimiento>(); }
		}

		public Table<RecursoCurricular.BaseCurricular.UnidadIndicador> UnidadIndicadores
		{
			get { return this.GetTable<RecursoCurricular.BaseCurricular.UnidadIndicador>(); }
		}

		public Table<RecursoCurricular.BaseCurricular.UnidadObjetivoAprendizaje> UnidadObjetivoAprendizajes
		{
			get { return this.GetTable<RecursoCurricular.BaseCurricular.UnidadObjetivoAprendizaje>(); }
		}

		public Table<RecursoCurricular.BaseCurricular.UnidadSubHabilidad> UnidadSubHabilidades
		{
			get { return this.GetTable<RecursoCurricular.BaseCurricular.UnidadSubHabilidad>(); }
		}
		#endregion

		#region Views
		#endregion

		#region StoredProcedures
		#endregion

		#region Functions
		#endregion

		#region Configuracion
		public class ActitudConfiguration : Mapping<RecursoCurricular.BaseCurricular.Actitud>
		{
			public ActitudConfiguration()
			{
				this.Named("BaseCurricular.Actitud");

				this.Map<Int32>(x => x.TipoEducacionCodigo).Named("TipoEducacionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.AnoNumero).Named("AnoNumero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.SectorId).Named("SectorId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.Numero).Named("Numero").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never);
				this.Map<String>(x => x.Descripcion).Named("Descripcion").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<RecursoCurricular.Anio>(x => x.Anio).ThisKey("AnoNumero").OtherKey("Numero").Storage("anio");
				this.HasOne<RecursoCurricular.Educacion.Sector>(x => x.Sector).ThisKey("SectorId").OtherKey("Id").Storage("sector");
				this.HasOne<RecursoCurricular.Educacion.TipoEducacion>(x => x.TipoEducacion).ThisKey("TipoEducacionCodigo").OtherKey("Codigo").Storage("tipoEducacion");
			}
		}

		public class AmbitoExperienciaAprendizajeConfiguration : Mapping<RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje>
		{
			public AmbitoExperienciaAprendizajeConfiguration()
			{
				this.Named("BaseCurricular.AmbitoExperienciaAprendizaje");

				this.Map<Int32>(x => x.AnoNumero).Named("AnoNumero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.Codigo).Named("Codigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<RecursoCurricular.Anio>(x => x.Anio).ThisKey("AnoNumero").OtherKey("Numero").Storage("anio");
			}
		}

		public class AprendizajeEsperadoParvuloConfiguration : Mapping<RecursoCurricular.BaseCurricular.AprendizajeEsperadoParvulo>
		{
			public AprendizajeEsperadoParvuloConfiguration()
			{
				this.Named("BaseCurricular.AprendizajeEsperadoParvulo");

				this.Map<Int32>(x => x.AnoNumero).Named("AnoNumero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.AmbitoExperienciaAprendizajeCodigo).Named("AmbitoExperienciaAprendizajeCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.NucleoAprendizajeId).Named("NucleoAprendizajeId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.CicloCodigo).Named("CicloCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Nullable<Guid>>(x => x.EjeParvuloId).Named("EjeParvuloId").UpdateCheck(UpdateCheck.Never);
				this.Map<Int32>(x => x.Numero).Named("Numero").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Descripcion).Named("Descripcion").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje>(x => x.AmbitoExperienciaAprendizaje).ThisKey("AnoNumero,AmbitoExperienciaAprendizajeCodigo").OtherKey("AnoNumero,Codigo").Storage("ambitoExperienciaAprendizaje");
				this.HasOne<RecursoCurricular.Anio>(x => x.Anio).ThisKey("AnoNumero").OtherKey("Numero").Storage("anio");
				this.HasOne<RecursoCurricular.Educacion.Ciclo>(x => x.Ciclo).ThisKey("CicloCodigo").OtherKey("Codigo").Storage("ciclo");
				this.HasOne<RecursoCurricular.BaseCurricular.EjeParvulo>(x => x.EjeParvulo).ThisKey("AnoNumero,AmbitoExperienciaAprendizajeCodigo,NucleoAprendizajeId,CicloCodigo,EjeParvuloId").OtherKey("AnoNumero,AmbitoExperienciaAprendizajeCodigo,NucleoId,CicloCodigo,Id").Storage("ejeParvulo");
				this.HasOne<RecursoCurricular.BaseCurricular.NucleoAprendizaje>(x => x.NucleoAprendizaje).ThisKey("AnoNumero,AmbitoExperienciaAprendizajeCodigo,NucleoAprendizajeId").OtherKey("AnoNumero,AmbitoExperienciaAprendizajeCodigo,Id").Storage("nucleoAprendizaje");
			}
		}

		public class ConocimientoConfiguration : Mapping<RecursoCurricular.BaseCurricular.Conocimiento>
		{
			public ConocimientoConfiguration()
			{
				this.Named("BaseCurricular.Conocimiento");

				this.Map<Int32>(x => x.TipoEducacionCodigo).Named("TipoEducacionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.AnoNumero).Named("AnoNumero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.SectorId).Named("SectorId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.Numero).Named("Numero").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never);
				this.Map<String>(x => x.Descripcion).Named("Descripcion").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<RecursoCurricular.Anio>(x => x.Anio).ThisKey("AnoNumero").OtherKey("Numero").Storage("anio");
				this.HasOne<RecursoCurricular.Educacion.Sector>(x => x.Sector).ThisKey("SectorId").OtherKey("Id").Storage("sector");
				this.HasOne<RecursoCurricular.Educacion.TipoEducacion>(x => x.TipoEducacion).ThisKey("TipoEducacionCodigo").OtherKey("Codigo").Storage("tipoEducacion");
			}
		}

		public class DimensionOATConfiguration : Mapping<RecursoCurricular.BaseCurricular.DimensionOAT>
		{
			public DimensionOATConfiguration()
			{
				this.Named("BaseCurricular.DimensionOAT");

				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.AnoNumero).Named("AnoNumero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.Numero).Named("Numero").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Descripcion).Named("Descripcion").UpdateCheck(UpdateCheck.Never);

				this.HasOne<RecursoCurricular.Anio>(x => x.Anio).ThisKey("AnoNumero").OtherKey("Numero").Storage("anio");
			}
		}

		public class EjeConfiguration : Mapping<RecursoCurricular.BaseCurricular.Eje>
		{
			public EjeConfiguration()
			{
				this.Named("BaseCurricular.Eje");

				this.Map<Int32>(x => x.AnoNumero).Named("AnoNumero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.SectorId).Named("SectorId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.Numero).Named("Numero").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<RecursoCurricular.Anio>(x => x.Anio).ThisKey("AnoNumero").OtherKey("Numero").Storage("anio");
				this.HasOne<RecursoCurricular.Educacion.Sector>(x => x.Sector).ThisKey("SectorId").OtherKey("Id").Storage("sector");
			}
		}

		public class EjeParvuloConfiguration : Mapping<RecursoCurricular.BaseCurricular.EjeParvulo>
		{
			public EjeParvuloConfiguration()
			{
				this.Named("BaseCurricular.EjeParvulo");

				this.Map<Int32>(x => x.AnoNumero).Named("AnoNumero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.AmbitoExperienciaAprendizajeCodigo).Named("AmbitoExperienciaAprendizajeCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.NucleoId).Named("NucleoId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.CicloCodigo).Named("CicloCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.Numero).Named("Numero").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje>(x => x.AmbitoExperienciaAprendizaje).ThisKey("AnoNumero,AmbitoExperienciaAprendizajeCodigo").OtherKey("AnoNumero,Codigo").Storage("ambitoExperienciaAprendizaje");
				this.HasOne<RecursoCurricular.Anio>(x => x.Anio).ThisKey("AnoNumero").OtherKey("Numero").Storage("anio");
				this.HasOne<RecursoCurricular.Educacion.Ciclo>(x => x.Ciclo).ThisKey("CicloCodigo").OtherKey("Codigo").Storage("ciclo");
				this.HasOne<RecursoCurricular.BaseCurricular.NucleoAprendizaje>(x => x.NucleoAprendizaje).ThisKey("AnoNumero,AmbitoExperienciaAprendizajeCodigo,NucleoId").OtherKey("AnoNumero,AmbitoExperienciaAprendizajeCodigo,Id").Storage("nucleoAprendizaje");
			}
		}

		public class HabilidadConfiguration : Mapping<RecursoCurricular.BaseCurricular.Habilidad>
		{
			public HabilidadConfiguration()
			{
				this.Named("BaseCurricular.Habilidad");

				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.TipoEducacionCodigo).Named("TipoEducacionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.AnoNumero).Named("AnoNumero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.SectorId).Named("SectorId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.Numero).Named("Numero").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Descripcion).Named("Descripcion").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<RecursoCurricular.Anio>(x => x.Anio).ThisKey("AnoNumero").OtherKey("Numero").Storage("anio");
				this.HasOne<RecursoCurricular.Educacion.Sector>(x => x.Sector).ThisKey("SectorId").OtherKey("Id").Storage("sector");
				this.HasOne<RecursoCurricular.Educacion.TipoEducacion>(x => x.TipoEducacion).ThisKey("TipoEducacionCodigo").OtherKey("Codigo").Storage("tipoEducacion");
			}
		}

		public class IndicadorConfiguration : Mapping<RecursoCurricular.BaseCurricular.Indicador>
		{
			public IndicadorConfiguration()
			{
				this.Named("BaseCurricular.Indicador");

				this.Map<Int32>(x => x.TipoEducacionCodigo).Named("TipoEducacionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.AnoNumero).Named("AnoNumero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.GradoCodigo).Named("GradoCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.SectorId).Named("SectorId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.EjeId).Named("EjeId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.ObjetivoAprendizajeId).Named("ObjetivoAprendizajeId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.Numero).Named("Numero").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Descripcion).Named("Descripcion").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<RecursoCurricular.Anio>(x => x.Anio).ThisKey("AnoNumero").OtherKey("Numero").Storage("anio");
				this.HasOne<RecursoCurricular.Educacion.Grado>(x => x.Grado).ThisKey("TipoEducacionCodigo,GradoCodigo").OtherKey("TipoEducacionCodigo,Codigo").Storage("grado");
				this.HasOne<RecursoCurricular.BaseCurricular.ObjetivoAprendizaje>(x => x.ObjetivoAprendizaje).ThisKey("TipoEducacionCodigo,AnoNumero,GradoCodigo,SectorId,EjeId,ObjetivoAprendizajeId").OtherKey("TipoEducacionCodigo,AnoNumero,GradoCodigo,SectorId,EjeId,Id").Storage("objetivoAprendizaje");
				this.HasOne<RecursoCurricular.Educacion.Sector>(x => x.Sector).ThisKey("SectorId").OtherKey("Id").Storage("sector");
				this.HasOne<RecursoCurricular.Educacion.TipoEducacion>(x => x.TipoEducacion).ThisKey("TipoEducacionCodigo").OtherKey("Codigo").Storage("tipoEducacion");
			}
		}

		public class NucleoAprendizajeConfiguration : Mapping<RecursoCurricular.BaseCurricular.NucleoAprendizaje>
		{
			public NucleoAprendizajeConfiguration()
			{
				this.Named("BaseCurricular.NucleoAprendizaje");

				this.Map<Int32>(x => x.AnoNumero).Named("AnoNumero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.AmbitoExperienciaAprendizajeCodigo).Named("AmbitoExperienciaAprendizajeCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.Numero).Named("Numero").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje>(x => x.AmbitoExperienciaAprendizaje).ThisKey("AnoNumero,AmbitoExperienciaAprendizajeCodigo").OtherKey("AnoNumero,Codigo").Storage("ambitoExperienciaAprendizaje");
				this.HasOne<RecursoCurricular.Anio>(x => x.Anio).ThisKey("AnoNumero").OtherKey("Numero").Storage("anio");
			}
		}

		public class ObjetivoAprendizajeConfiguration : Mapping<RecursoCurricular.BaseCurricular.ObjetivoAprendizaje>
		{
			public ObjetivoAprendizajeConfiguration()
			{
				this.Named("BaseCurricular.ObjetivoAprendizaje");

				this.Map<Int32>(x => x.TipoEducacionCodigo).Named("TipoEducacionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.AnoNumero).Named("AnoNumero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.GradoCodigo).Named("GradoCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.SectorId).Named("SectorId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.EjeId).Named("EjeId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.Numero).Named("Numero").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Descripcion).Named("Descripcion").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<RecursoCurricular.Anio>(x => x.Anio).ThisKey("AnoNumero").OtherKey("Numero").Storage("anio");
				this.HasOne<RecursoCurricular.BaseCurricular.Eje>(x => x.Eje).ThisKey("AnoNumero,SectorId,EjeId").OtherKey("AnoNumero,SectorId,Id").Storage("eje");
				this.HasOne<RecursoCurricular.Educacion.Grado>(x => x.Grado).ThisKey("TipoEducacionCodigo,GradoCodigo").OtherKey("TipoEducacionCodigo,Codigo").Storage("grado");
				this.HasOne<RecursoCurricular.Educacion.Sector>(x => x.Sector).ThisKey("SectorId").OtherKey("Id").Storage("sector");
				this.HasOne<RecursoCurricular.Educacion.TipoEducacion>(x => x.TipoEducacion).ThisKey("TipoEducacionCodigo").OtherKey("Codigo").Storage("tipoEducacion");
			}
		}

		public class ObjetivoAprendizajeTransversalConfiguration : Mapping<RecursoCurricular.BaseCurricular.ObjetivoAprendizajeTransversal>
		{
			public ObjetivoAprendizajeTransversalConfiguration()
			{
				this.Named("BaseCurricular.ObjetivoAprendizajeTransversal");

				this.Map<Guid>(x => x.DimensionOATId).Named("DimensionOATId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.AnoNumero).Named("AnoNumero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.Numero).Named("Numero").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Descripcion).Named("Descripcion").UpdateCheck(UpdateCheck.Never);

				this.HasOne<RecursoCurricular.Anio>(x => x.Anio).ThisKey("AnoNumero").OtherKey("Numero").Storage("anio");
				this.HasOne<RecursoCurricular.BaseCurricular.DimensionOAT>(x => x.DimensionOAT).ThisKey("DimensionOATId,AnoNumero").OtherKey("Id,AnoNumero").Storage("dimensionOAT");
			}
		}

		public class PrincipioPedagogicoConfiguration : Mapping<RecursoCurricular.BaseCurricular.PrincipioPedagogico>
		{
			public PrincipioPedagogicoConfiguration()
			{
				this.Named("BaseCurricular.PrincipioPedagogico");

				this.Map<Int32>(x => x.AnoNumero).Named("AnoNumero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.Codigo).Named("Codigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Detalle).Named("Detalle").UpdateCheck(UpdateCheck.Never);

				this.HasOne<RecursoCurricular.Anio>(x => x.Anio).ThisKey("AnoNumero").OtherKey("Numero").Storage("anio");
			}
		}

		public class SubHabilidadConfiguration : Mapping<RecursoCurricular.BaseCurricular.SubHabilidad>
		{
			public SubHabilidadConfiguration()
			{
				this.Named("BaseCurricular.SubHabilidad");

				this.Map<Int32>(x => x.TipoEducacionCodigo).Named("TipoEducacionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.AnoNumero).Named("AnoNumero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.GradoCodigo).Named("GradoCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.HabilidadId).Named("HabilidadId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.SectorId).Named("SectorId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.Numero).Named("Numero").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Descripcion).Named("Descripcion").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<RecursoCurricular.Anio>(x => x.Anio).ThisKey("AnoNumero").OtherKey("Numero").Storage("anio");
				this.HasOne<RecursoCurricular.Educacion.Grado>(x => x.Grado).ThisKey("TipoEducacionCodigo,GradoCodigo").OtherKey("TipoEducacionCodigo,Codigo").Storage("grado");
				this.HasOne<RecursoCurricular.BaseCurricular.Habilidad>(x => x.Habilidad).ThisKey("HabilidadId,TipoEducacionCodigo,AnoNumero,SectorId").OtherKey("Id,TipoEducacionCodigo,AnoNumero,SectorId").Storage("habilidad");
				this.HasOne<RecursoCurricular.Educacion.Sector>(x => x.Sector).ThisKey("SectorId").OtherKey("Id").Storage("sector");
				this.HasOne<RecursoCurricular.Educacion.TipoEducacion>(x => x.TipoEducacion).ThisKey("TipoEducacionCodigo").OtherKey("Codigo").Storage("tipoEducacion");
			}
		}

		public class TipoEducacionEjeConfiguration : Mapping<RecursoCurricular.BaseCurricular.TipoEducacionEje>
		{
			public TipoEducacionEjeConfiguration()
			{
				this.Named("BaseCurricular.TipoEducacionEje");

				this.Map<Int32>(x => x.TipoEducacionCodigo).Named("TipoEducacionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.AnoNumero).Named("AnoNumero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.SectorId).Named("SectorId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.EjeId).Named("EjeId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();

				this.HasOne<RecursoCurricular.Anio>(x => x.Anio).ThisKey("AnoNumero").OtherKey("Numero").Storage("anio");
				this.HasOne<RecursoCurricular.BaseCurricular.Eje>(x => x.Eje).ThisKey("AnoNumero,SectorId,EjeId").OtherKey("AnoNumero,SectorId,Id").Storage("eje");
				this.HasOne<RecursoCurricular.Educacion.TipoEducacion>(x => x.TipoEducacion).ThisKey("TipoEducacionCodigo").OtherKey("Codigo").Storage("tipoEducacion");
			}
		}

		public class UnidadConfiguration : Mapping<RecursoCurricular.BaseCurricular.Unidad>
		{
			public UnidadConfiguration()
			{
				this.Named("BaseCurricular.Unidad");

				this.Map<Int32>(x => x.TipoEducacionCodigo).Named("TipoEducacionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.AnoNumero).Named("AnoNumero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.GradoCodigo).Named("GradoCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.SectorId).Named("SectorId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Proposito).Named("Proposito").UpdateCheck(UpdateCheck.Never);
				this.Map<String>(x => x.ConocimientoPrevio).Named("ConocimientoPrevio").UpdateCheck(UpdateCheck.Never);
				this.Map<String>(x => x.PalabraClave).Named("PalabraClave").UpdateCheck(UpdateCheck.Never);
				this.Map<Int32>(x => x.Numero).Named("Numero").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<RecursoCurricular.Anio>(x => x.Anio).ThisKey("AnoNumero").OtherKey("Numero").Storage("anio");
				this.HasOne<RecursoCurricular.Educacion.Grado>(x => x.Grado).ThisKey("TipoEducacionCodigo,GradoCodigo").OtherKey("TipoEducacionCodigo,Codigo").Storage("grado");
				this.HasOne<RecursoCurricular.Educacion.Sector>(x => x.Sector).ThisKey("SectorId").OtherKey("Id").Storage("sector");
				this.HasOne<RecursoCurricular.Educacion.TipoEducacion>(x => x.TipoEducacion).ThisKey("TipoEducacionCodigo").OtherKey("Codigo").Storage("tipoEducacion");
			}
		}

		public class UnidadActitudConfiguration : Mapping<RecursoCurricular.BaseCurricular.UnidadActitud>
		{
			public UnidadActitudConfiguration()
			{
				this.Named("BaseCurricular.UnidadActitud");

				this.Map<Int32>(x => x.TipoEducacionCodigo).Named("TipoEducacionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.AnoNumero).Named("AnoNumero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.GradoCodigo).Named("GradoCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.SectorId).Named("SectorId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.UnidadId).Named("UnidadId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.ActitudId).Named("ActitudId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();

				this.HasOne<RecursoCurricular.BaseCurricular.Actitud>(x => x.Actitud).ThisKey("TipoEducacionCodigo,AnoNumero,SectorId,ActitudId").OtherKey("TipoEducacionCodigo,AnoNumero,SectorId,Id").Storage("actitud");
				this.HasOne<RecursoCurricular.Anio>(x => x.Anio).ThisKey("AnoNumero").OtherKey("Numero").Storage("anio");
				this.HasOne<RecursoCurricular.BaseCurricular.Unidad>(x => x.Unidad).ThisKey("TipoEducacionCodigo,AnoNumero,GradoCodigo,SectorId,UnidadId").OtherKey("TipoEducacionCodigo,AnoNumero,GradoCodigo,SectorId,Id").Storage("unidad");
			}
		}

		public class UnidadConocimientoConfiguration : Mapping<RecursoCurricular.BaseCurricular.UnidadConocimiento>
		{
			public UnidadConocimientoConfiguration()
			{
				this.Named("BaseCurricular.UnidadConocimiento");

				this.Map<Int32>(x => x.TipoEducacionCodigo).Named("TipoEducacionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.AnoNumero).Named("AnoNumero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.GradoCodigo).Named("GradoCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.SectorId).Named("SectorId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.UnidadId).Named("UnidadId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.ConocimientoId).Named("ConocimientoId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();

				this.HasOne<RecursoCurricular.Anio>(x => x.Anio).ThisKey("AnoNumero").OtherKey("Numero").Storage("anio");
				this.HasOne<RecursoCurricular.BaseCurricular.Conocimiento>(x => x.Conocimiento).ThisKey("TipoEducacionCodigo,AnoNumero,SectorId,ConocimientoId").OtherKey("TipoEducacionCodigo,AnoNumero,SectorId,Id").Storage("conocimiento");
				this.HasOne<RecursoCurricular.BaseCurricular.Unidad>(x => x.Unidad).ThisKey("TipoEducacionCodigo,AnoNumero,GradoCodigo,SectorId,UnidadId").OtherKey("TipoEducacionCodigo,AnoNumero,GradoCodigo,SectorId,Id").Storage("unidad");
			}
		}

		public class UnidadIndicadorConfiguration : Mapping<RecursoCurricular.BaseCurricular.UnidadIndicador>
		{
			public UnidadIndicadorConfiguration()
			{
				this.Named("BaseCurricular.UnidadIndicador");

				this.Map<Int32>(x => x.TipoEducacionCodigo).Named("TipoEducacionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.AnoNumero).Named("AnoNumero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.GradoCodigo).Named("GradoCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.SectorId).Named("SectorId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.UnidadId).Named("UnidadId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.EjeId).Named("EjeId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.ObjetivoAprendizajeId).Named("ObjetivoAprendizajeId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.IndicadorId).Named("IndicadorId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.Orden).Named("Orden").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<RecursoCurricular.BaseCurricular.Indicador>(x => x.Indicador).ThisKey("TipoEducacionCodigo,AnoNumero,GradoCodigo,SectorId,EjeId,ObjetivoAprendizajeId,IndicadorId").OtherKey("TipoEducacionCodigo,AnoNumero,GradoCodigo,SectorId,EjeId,ObjetivoAprendizajeId,Id").Storage("indicador");
				this.HasOne<RecursoCurricular.BaseCurricular.Unidad>(x => x.Unidad).ThisKey("TipoEducacionCodigo,AnoNumero,GradoCodigo,SectorId,UnidadId").OtherKey("TipoEducacionCodigo,AnoNumero,GradoCodigo,SectorId,Id").Storage("unidad");
				this.HasOne<RecursoCurricular.BaseCurricular.UnidadObjetivoAprendizaje>(x => x.UnidadObjetivoAprendizaje).ThisKey("TipoEducacionCodigo,AnoNumero,GradoCodigo,SectorId,UnidadId,EjeId,ObjetivoAprendizajeId").OtherKey("TipoEducacionCodigo,AnoNumero,GradoCodigo,SectorId,UnidadId,EjeId,ObjetivoAprendizajeId").Storage("unidadObjetivoAprendizaje");
			}
		}

		public class UnidadObjetivoAprendizajeConfiguration : Mapping<RecursoCurricular.BaseCurricular.UnidadObjetivoAprendizaje>
		{
			public UnidadObjetivoAprendizajeConfiguration()
			{
				this.Named("BaseCurricular.UnidadObjetivoAprendizaje");

				this.Map<Int32>(x => x.TipoEducacionCodigo).Named("TipoEducacionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.AnoNumero).Named("AnoNumero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.GradoCodigo).Named("GradoCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.SectorId).Named("SectorId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.UnidadId).Named("UnidadId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.EjeId).Named("EjeId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.ObjetivoAprendizajeId).Named("ObjetivoAprendizajeId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.Orden).Named("Orden").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<RecursoCurricular.Anio>(x => x.Anio).ThisKey("AnoNumero").OtherKey("Numero").Storage("anio");
				this.HasOne<RecursoCurricular.BaseCurricular.ObjetivoAprendizaje>(x => x.ObjetivoAprendizaje).ThisKey("TipoEducacionCodigo,AnoNumero,GradoCodigo,SectorId,EjeId,ObjetivoAprendizajeId").OtherKey("TipoEducacionCodigo,AnoNumero,GradoCodigo,SectorId,EjeId,Id").Storage("objetivoAprendizaje");
				this.HasOne<RecursoCurricular.BaseCurricular.Unidad>(x => x.Unidad).ThisKey("TipoEducacionCodigo,AnoNumero,GradoCodigo,SectorId,UnidadId").OtherKey("TipoEducacionCodigo,AnoNumero,GradoCodigo,SectorId,Id").Storage("unidad");
			}
		}

		public class UnidadSubHabilidadConfiguration : Mapping<RecursoCurricular.BaseCurricular.UnidadSubHabilidad>
		{
			public UnidadSubHabilidadConfiguration()
			{
				this.Named("BaseCurricular.UnidadSubHabilidad");

				this.Map<Int32>(x => x.TipoEducacionCodigo).Named("TipoEducacionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.AnoNumero).Named("AnoNumero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.GradoCodigo).Named("GradoCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.SectorId).Named("SectorId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.UnidadId).Named("UnidadId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.HabilidadId).Named("HabilidadId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.SubHabilidadId).Named("SubHabilidadId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();

				this.HasOne<RecursoCurricular.BaseCurricular.SubHabilidad>(x => x.SubHabilidad).ThisKey("TipoEducacionCodigo,AnoNumero,GradoCodigo,HabilidadId,SectorId,SubHabilidadId").OtherKey("TipoEducacionCodigo,AnoNumero,GradoCodigo,HabilidadId,SectorId,Id").Storage("subHabilidad");
				this.HasOne<RecursoCurricular.BaseCurricular.Unidad>(x => x.Unidad).ThisKey("TipoEducacionCodigo,AnoNumero,GradoCodigo,SectorId,UnidadId").OtherKey("TipoEducacionCodigo,AnoNumero,GradoCodigo,SectorId,Id").Storage("unidad");
			}
		}
		#endregion
	}
}