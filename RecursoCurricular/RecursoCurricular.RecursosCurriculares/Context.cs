using System;
using System.Configuration;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using System.Web;
using System.Web.Hosting;
using FluentLinqToSql;
namespace RecursoCurricular.RecursosCurriculares
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
					if (HttpContext.Current.Items["RecursosCurriculares_Context"] == null)
					{
						HttpContext.Current.Items["RecursosCurriculares_Context"] = new Context();
					}

					return (Context)HttpContext.Current.Items["RecursosCurriculares_Context"];
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
				.AddFromAssemblyContaining<RecursoCurricular.Educacion.Context>()
				.AddFromAssemblyContaining<RecursoCurricular.RecursosCurriculares.Context>()
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

			if (HostingEnvironment.IsHosted) HttpContext.Current.Items["RecursosCurriculares_Context"] = null;
			else Context.instancia = null;
		}

		#region Tables
		public Table<RecursoCurricular.RecursosCurriculares.Aprendizaje> Aprendizajes
		{
			get { return this.GetTable<RecursoCurricular.RecursosCurriculares.Aprendizaje>(); }
		}

		public Table<RecursoCurricular.RecursosCurriculares.AprendizajeContenido> AprendizajeContenidos
		{
			get { return this.GetTable<RecursoCurricular.RecursosCurriculares.AprendizajeContenido>(); }
		}

		public Table<RecursoCurricular.RecursosCurriculares.AprendizajeIndicador> AprendizajeIndicadores
		{
			get { return this.GetTable<RecursoCurricular.RecursosCurriculares.AprendizajeIndicador>(); }
		}

		public Table<RecursoCurricular.RecursosCurriculares.AprendizajeObjetivoVertical> AprendizajeObjetivoVerticales
		{
			get { return this.GetTable<RecursoCurricular.RecursosCurriculares.AprendizajeObjetivoVertical>(); }
		}

		public Table<RecursoCurricular.RecursosCurriculares.Categoria> Categorias
		{
			get { return this.GetTable<RecursoCurricular.RecursosCurriculares.Categoria>(); }
		}

		public Table<RecursoCurricular.RecursosCurriculares.Contenido> Contenidos
		{
			get { return this.GetTable<RecursoCurricular.RecursosCurriculares.Contenido>(); }
		}

		public Table<RecursoCurricular.RecursosCurriculares.Eje> Ejes
		{
			get { return this.GetTable<RecursoCurricular.RecursosCurriculares.Eje>(); }
		}

		public Table<RecursoCurricular.RecursosCurriculares.ObjetivoTransversal> ObjetivoTransversales
		{
			get { return this.GetTable<RecursoCurricular.RecursosCurriculares.ObjetivoTransversal>(); }
		}

		public Table<RecursoCurricular.RecursosCurriculares.ObjetivoTransversalIndicador> ObjetivoTransversalIndicadores
		{
			get { return this.GetTable<RecursoCurricular.RecursosCurriculares.ObjetivoTransversalIndicador>(); }
		}

		public Table<RecursoCurricular.RecursosCurriculares.ObjetivoVertical> ObjetivoVerticales
		{
			get { return this.GetTable<RecursoCurricular.RecursosCurriculares.ObjetivoVertical>(); }
		}

		public Table<RecursoCurricular.RecursosCurriculares.TipoEducacionEje> TipoEducacionEjes
		{
			get { return this.GetTable<RecursoCurricular.RecursosCurriculares.TipoEducacionEje>(); }
		}

		public Table<RecursoCurricular.RecursosCurriculares.Unidad> Unidades
		{
			get { return this.GetTable<RecursoCurricular.RecursosCurriculares.Unidad>(); }
		}

		public Table<RecursoCurricular.RecursosCurriculares.UnidadAprendizaje> UnidadAprendizajes
		{
			get { return this.GetTable<RecursoCurricular.RecursosCurriculares.UnidadAprendizaje>(); }
		}
		#endregion

		#region Views
		#endregion

		#region StoredProcedures
		#endregion

		#region Functions
		#endregion

		#region Configuracion
		public class AprendizajeConfiguration : Mapping<RecursoCurricular.RecursosCurriculares.Aprendizaje>
		{
			public AprendizajeConfiguration()
			{
				this.Named("RecursosCurriculares.Aprendizaje");

				this.Map<Int32>(x => x.AnoNumero).Named("AnoNumero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.TipoEducacionCodigo).Named("TipoEducacionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.GradoCodigo).Named("GradoCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.SectorId).Named("SectorId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.Numero).Named("Numero").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Descripcion).Named("Descripcion").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<RecursoCurricular.Anio>(x => x.Anio).ThisKey("AnoNumero").OtherKey("Numero").Storage("anio");
				this.HasOne<RecursoCurricular.Educacion.Grado>(x => x.Grado).ThisKey("TipoEducacionCodigo,GradoCodigo").OtherKey("TipoEducacionCodigo,Codigo").Storage("grado");
				this.HasOne<RecursoCurricular.Educacion.Sector>(x => x.Sector).ThisKey("SectorId").OtherKey("Id").Storage("sector");
				this.HasOne<RecursoCurricular.Educacion.TipoEducacion>(x => x.TipoEducacion).ThisKey("TipoEducacionCodigo").OtherKey("Codigo").Storage("tipoEducacion");
			}
		}

		public class AprendizajeContenidoConfiguration : Mapping<RecursoCurricular.RecursosCurriculares.AprendizajeContenido>
		{
			public AprendizajeContenidoConfiguration()
			{
				this.Named("RecursosCurriculares.AprendizajeContenido");

				this.Map<Int32>(x => x.AnoNumero).Named("AnoNumero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.TipoEducacionCodigo).Named("TipoEducacionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.GradoCodigo).Named("GradoCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.SectorId).Named("SectorId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.AprendizajeId).Named("AprendizajeId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.EjeId).Named("EjeId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.ContenidoId).Named("ContenidoId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();

				this.HasOne<RecursoCurricular.Anio>(x => x.Anio).ThisKey("AnoNumero").OtherKey("Numero").Storage("anio");
				this.HasOne<RecursoCurricular.RecursosCurriculares.Aprendizaje>(x => x.Aprendizaje).ThisKey("AnoNumero,TipoEducacionCodigo,GradoCodigo,SectorId,AprendizajeId").OtherKey("AnoNumero,TipoEducacionCodigo,GradoCodigo,SectorId,Id").Storage("aprendizaje");
				this.HasOne<RecursoCurricular.RecursosCurriculares.Contenido>(x => x.Contenido).ThisKey("AnoNumero,TipoEducacionCodigo,SectorId,EjeId,GradoCodigo,ContenidoId").OtherKey("AnoNumero,TipoEducacionCodigo,SectorId,EjeId,GradoCodigo,Id").Storage("contenido");
				this.HasOne<RecursoCurricular.Educacion.Grado>(x => x.Grado).ThisKey("TipoEducacionCodigo,GradoCodigo").OtherKey("TipoEducacionCodigo,Codigo").Storage("grado");
				this.HasOne<RecursoCurricular.Educacion.Sector>(x => x.Sector).ThisKey("SectorId").OtherKey("Id").Storage("sector");
			}
		}

		public class AprendizajeIndicadorConfiguration : Mapping<RecursoCurricular.RecursosCurriculares.AprendizajeIndicador>
		{
			public AprendizajeIndicadorConfiguration()
			{
				this.Named("RecursosCurriculares.AprendizajeIndicador");

				this.Map<Int32>(x => x.AnoNumero).Named("AnoNumero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.TipoEducacionCodigo).Named("TipoEducacionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.GradoCodigo).Named("GradoCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.SectorId).Named("SectorId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.AprendizajeId).Named("AprendizajeId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Nullable<Int32>>(x => x.CategoriaCodigo).Named("CategoriaCodigo").UpdateCheck(UpdateCheck.Never);
				this.Map<Int32>(x => x.Numero).Named("Numero").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Descripcion).Named("Descripcion").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<RecursoCurricular.Anio>(x => x.Anio).ThisKey("AnoNumero").OtherKey("Numero").Storage("anio");
				this.HasOne<RecursoCurricular.RecursosCurriculares.Aprendizaje>(x => x.Aprendizaje).ThisKey("AnoNumero,TipoEducacionCodigo,GradoCodigo,SectorId,AprendizajeId").OtherKey("AnoNumero,TipoEducacionCodigo,GradoCodigo,SectorId,Id").Storage("aprendizaje");
				this.HasOne<RecursoCurricular.RecursosCurriculares.Categoria>(x => x.Categoria).ThisKey("CategoriaCodigo").OtherKey("Codigo").Storage("categoria");
				this.HasOne<RecursoCurricular.Educacion.Grado>(x => x.Grado).ThisKey("TipoEducacionCodigo,GradoCodigo").OtherKey("TipoEducacionCodigo,Codigo").Storage("grado");
				this.HasOne<RecursoCurricular.Educacion.Sector>(x => x.Sector).ThisKey("SectorId").OtherKey("Id").Storage("sector");
				this.HasOne<RecursoCurricular.Educacion.TipoEducacion>(x => x.TipoEducacion).ThisKey("TipoEducacionCodigo").OtherKey("Codigo").Storage("tipoEducacion");
			}
		}

		public class AprendizajeObjetivoVerticalConfiguration : Mapping<RecursoCurricular.RecursosCurriculares.AprendizajeObjetivoVertical>
		{
			public AprendizajeObjetivoVerticalConfiguration()
			{
				this.Named("RecursosCurriculares.AprendizajeObjetivoVertical");

				this.Map<Int32>(x => x.AnoNumero).Named("AnoNumero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.TipoEducacionCodigo).Named("TipoEducacionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.GradoCodigo).Named("GradoCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.SectorId).Named("SectorId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.AprendizajeId).Named("AprendizajeId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.ObjetivoVerticalId).Named("ObjetivoVerticalId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();

				this.HasOne<RecursoCurricular.Anio>(x => x.Anio).ThisKey("AnoNumero").OtherKey("Numero").Storage("anio");
				this.HasOne<RecursoCurricular.RecursosCurriculares.Aprendizaje>(x => x.Aprendizaje).ThisKey("AnoNumero,TipoEducacionCodigo,GradoCodigo,SectorId,AprendizajeId").OtherKey("AnoNumero,TipoEducacionCodigo,GradoCodigo,SectorId,Id").Storage("aprendizaje");
				this.HasOne<RecursoCurricular.RecursosCurriculares.ObjetivoVertical>(x => x.ObjetivoVertical).ThisKey("AnoNumero,TipoEducacionCodigo,GradoCodigo,SectorId,ObjetivoVerticalId").OtherKey("AnoNumero,TipoEducacionCodigo,GradoCodigo,SectorId,Id").Storage("objetivoVertical");
			}
		}

		public class CategoriaConfiguration : Mapping<RecursoCurricular.RecursosCurriculares.Categoria>
		{
			public CategoriaConfiguration()
			{
				this.Named("RecursosCurriculares.Categoria");

				this.Map<Int32>(x => x.Codigo).Named("Codigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();

			}
		}

		public class ContenidoConfiguration : Mapping<RecursoCurricular.RecursosCurriculares.Contenido>
		{
			public ContenidoConfiguration()
			{
				this.Named("RecursosCurriculares.Contenido");

				this.Map<Int32>(x => x.AnoNumero).Named("AnoNumero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.TipoEducacionCodigo).Named("TipoEducacionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.SectorId).Named("SectorId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.EjeId).Named("EjeId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.GradoCodigo).Named("GradoCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.Numero).Named("Numero").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Descripcion).Named("Descripcion").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Boolean>(x => x.Transversal).Named("Transversal").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<RecursoCurricular.Anio>(x => x.Anio).ThisKey("AnoNumero").OtherKey("Numero").Storage("anio");
				this.HasOne<RecursoCurricular.RecursosCurriculares.Eje>(x => x.Eje).ThisKey("AnoNumero,SectorId,EjeId").OtherKey("AnoNumero,SectorId,Id").Storage("eje");
				this.HasOne<RecursoCurricular.Educacion.Grado>(x => x.Grado).ThisKey("TipoEducacionCodigo,GradoCodigo").OtherKey("TipoEducacionCodigo,Codigo").Storage("grado");
				this.HasOne<RecursoCurricular.Educacion.Sector>(x => x.Sector).ThisKey("SectorId").OtherKey("Id").Storage("sector");
				this.HasOne<RecursoCurricular.Educacion.TipoEducacion>(x => x.TipoEducacion).ThisKey("TipoEducacionCodigo").OtherKey("Codigo").Storage("tipoEducacion");
			}
		}

		public class EjeConfiguration : Mapping<RecursoCurricular.RecursosCurriculares.Eje>
		{
			public EjeConfiguration()
			{
				this.Named("RecursosCurriculares.Eje");

				this.Map<Int32>(x => x.AnoNumero).Named("AnoNumero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.SectorId).Named("SectorId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.Numero).Named("Numero").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<RecursoCurricular.Anio>(x => x.Anio).ThisKey("AnoNumero").OtherKey("Numero").Storage("anio");
				this.HasOne<RecursoCurricular.Educacion.Sector>(x => x.Sector).ThisKey("SectorId").OtherKey("Id").Storage("sector");
			}
		}

		public class ObjetivoTransversalConfiguration : Mapping<RecursoCurricular.RecursosCurriculares.ObjetivoTransversal>
		{
			public ObjetivoTransversalConfiguration()
			{
				this.Named("RecursosCurriculares.ObjetivoTransversal");

				this.Map<Int32>(x => x.AnoNumero).Named("AnoNumero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.TipoEducacionCodigo).Named("TipoEducacionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.GradoCodigo).Named("GradoCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.SectorId).Named("SectorId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.UnidadId).Named("UnidadId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.Numero).Named("Numero").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Descripcion).Named("Descripcion").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<RecursoCurricular.Educacion.Grado>(x => x.Grado).ThisKey("TipoEducacionCodigo,GradoCodigo").OtherKey("TipoEducacionCodigo,Codigo").Storage("grado");
				this.HasOne<RecursoCurricular.Educacion.Sector>(x => x.Sector).ThisKey("SectorId").OtherKey("Id").Storage("sector");
				this.HasOne<RecursoCurricular.RecursosCurriculares.Unidad>(x => x.Unidad).ThisKey("AnoNumero,TipoEducacionCodigo,GradoCodigo,SectorId,UnidadId").OtherKey("AnoNumero,TipoEducacionCodigo,GradoCodigo,SectorId,Id").Storage("unidad");
			}
		}

		public class ObjetivoTransversalIndicadorConfiguration : Mapping<RecursoCurricular.RecursosCurriculares.ObjetivoTransversalIndicador>
		{
			public ObjetivoTransversalIndicadorConfiguration()
			{
				this.Named("RecursosCurriculares.ObjetivoTransversalIndicador");

				this.Map<Int32>(x => x.AnoNumero).Named("AnoNumero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.TipoEducacionCodigo).Named("TipoEducacionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.GradoCodigo).Named("GradoCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.SectorId).Named("SectorId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.UnidadId).Named("UnidadId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.ObjetivoTransversalId).Named("ObjetivoTransversalId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.Numero).Named("Numero").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Descripcion).Named("Descripcion").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<RecursoCurricular.Anio>(x => x.Anio).ThisKey("AnoNumero").OtherKey("Numero").Storage("anio");
				this.HasOne<RecursoCurricular.Educacion.Grado>(x => x.Grado).ThisKey("TipoEducacionCodigo,GradoCodigo").OtherKey("TipoEducacionCodigo,Codigo").Storage("grado");
				this.HasOne<RecursoCurricular.RecursosCurriculares.ObjetivoTransversal>(x => x.ObjetivoTransversal).ThisKey("AnoNumero,TipoEducacionCodigo,GradoCodigo,SectorId,UnidadId,ObjetivoTransversalId").OtherKey("AnoNumero,TipoEducacionCodigo,GradoCodigo,SectorId,UnidadId,Id").Storage("objetivoTransversal");
				this.HasOne<RecursoCurricular.Educacion.Sector>(x => x.Sector).ThisKey("SectorId").OtherKey("Id").Storage("sector");
				this.HasOne<RecursoCurricular.Educacion.TipoEducacion>(x => x.TipoEducacion).ThisKey("TipoEducacionCodigo").OtherKey("Codigo").Storage("tipoEducacion");
				this.HasOne<RecursoCurricular.RecursosCurriculares.Unidad>(x => x.Unidad).ThisKey("AnoNumero,TipoEducacionCodigo,GradoCodigo,SectorId,UnidadId").OtherKey("AnoNumero,TipoEducacionCodigo,GradoCodigo,SectorId,Id").Storage("unidad");
			}
		}

		public class ObjetivoVerticalConfiguration : Mapping<RecursoCurricular.RecursosCurriculares.ObjetivoVertical>
		{
			public ObjetivoVerticalConfiguration()
			{
				this.Named("RecursosCurriculares.ObjetivoVertical");

				this.Map<Int32>(x => x.AnoNumero).Named("AnoNumero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.TipoEducacionCodigo).Named("TipoEducacionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.GradoCodigo).Named("GradoCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.SectorId).Named("SectorId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.Numero).Named("Numero").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Descripcion).Named("Descripcion").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<RecursoCurricular.Anio>(x => x.Anio).ThisKey("AnoNumero").OtherKey("Numero").Storage("anio");
				this.HasOne<RecursoCurricular.Educacion.Grado>(x => x.Grado).ThisKey("TipoEducacionCodigo,GradoCodigo").OtherKey("TipoEducacionCodigo,Codigo").Storage("grado");
				this.HasOne<RecursoCurricular.Educacion.Sector>(x => x.Sector).ThisKey("SectorId").OtherKey("Id").Storage("sector");
			}
		}

		public class TipoEducacionEjeConfiguration : Mapping<RecursoCurricular.RecursosCurriculares.TipoEducacionEje>
		{
			public TipoEducacionEjeConfiguration()
			{
				this.Named("RecursosCurriculares.TipoEducacionEje");

				this.Map<Int32>(x => x.TipoEducacionCodigo).Named("TipoEducacionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.AnoNumero).Named("AnoNumero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.SectorId).Named("SectorId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.EjeId).Named("EjeId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();

				this.HasOne<RecursoCurricular.RecursosCurriculares.Eje>(x => x.Eje).ThisKey("AnoNumero,SectorId,EjeId").OtherKey("AnoNumero,SectorId,Id").Storage("eje");
				this.HasOne<RecursoCurricular.Educacion.TipoEducacion>(x => x.TipoEducacion).ThisKey("TipoEducacionCodigo").OtherKey("Codigo").Storage("tipoEducacion");
			}
		}

		public class UnidadConfiguration : Mapping<RecursoCurricular.RecursosCurriculares.Unidad>
		{
			public UnidadConfiguration()
			{
				this.Named("RecursosCurriculares.Unidad");

				this.Map<Int32>(x => x.AnoNumero).Named("AnoNumero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.TipoEducacionCodigo).Named("TipoEducacionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.GradoCodigo).Named("GradoCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.SectorId).Named("SectorId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.Numero).Named("Numero").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<RecursoCurricular.Anio>(x => x.Anio).ThisKey("AnoNumero").OtherKey("Numero").Storage("anio");
				this.HasOne<RecursoCurricular.Educacion.Grado>(x => x.Grado).ThisKey("TipoEducacionCodigo,GradoCodigo").OtherKey("TipoEducacionCodigo,Codigo").Storage("grado");
				this.HasOne<RecursoCurricular.Educacion.Sector>(x => x.Sector).ThisKey("SectorId").OtherKey("Id").Storage("sector");
				this.HasOne<RecursoCurricular.Educacion.TipoEducacion>(x => x.TipoEducacion).ThisKey("TipoEducacionCodigo").OtherKey("Codigo").Storage("tipoEducacion");
			}
		}

		public class UnidadAprendizajeConfiguration : Mapping<RecursoCurricular.RecursosCurriculares.UnidadAprendizaje>
		{
			public UnidadAprendizajeConfiguration()
			{
				this.Named("RecursosCurriculares.UnidadAprendizaje");

				this.Map<Int32>(x => x.AnoNumero).Named("AnoNumero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.TipoEducacionCodigo).Named("TipoEducacionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.GradoCodigo).Named("GradoCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.SectorId).Named("SectorId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.UnidadId).Named("UnidadId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.AprendizajeId).Named("AprendizajeId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();

				this.HasOne<RecursoCurricular.Anio>(x => x.Anio).ThisKey("AnoNumero").OtherKey("Numero").Storage("anio");
				this.HasOne<RecursoCurricular.RecursosCurriculares.Aprendizaje>(x => x.Aprendizaje).ThisKey("AnoNumero,TipoEducacionCodigo,GradoCodigo,SectorId,AprendizajeId").OtherKey("AnoNumero,TipoEducacionCodigo,GradoCodigo,SectorId,Id").Storage("aprendizaje");
				this.HasOne<RecursoCurricular.Educacion.Grado>(x => x.Grado).ThisKey("TipoEducacionCodigo,GradoCodigo").OtherKey("TipoEducacionCodigo,Codigo").Storage("grado");
				this.HasOne<RecursoCurricular.Educacion.Sector>(x => x.Sector).ThisKey("SectorId").OtherKey("Id").Storage("sector");
				this.HasOne<RecursoCurricular.RecursosCurriculares.Unidad>(x => x.Unidad).ThisKey("AnoNumero,TipoEducacionCodigo,GradoCodigo,SectorId,UnidadId").OtherKey("AnoNumero,TipoEducacionCodigo,GradoCodigo,SectorId,Id").Storage("unidad");
			}
		}
		#endregion
	}
}