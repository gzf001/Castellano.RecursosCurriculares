using System.Web.Mvc;

namespace RecursoCurricular.Web.UI.Areas.RecursosCurriculares
{
    public class RecursosCurricularesAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "RecursosCurriculares";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "RecursosCurriculares_default",
                "RecursosCurriculares/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            #region Eje

            context.MapRoute(
                   name: "GetEjesRecursosCurriculares",
                   url: "RecursosCurriculares/Eje/GetEjes/{sectorId}",
                   defaults: new { area = "RecursosCurriculares", controller = "Eje", action = "GetEjes" }
               );

            context.MapRoute(
                   name: "GetEjesComboRecursosCurriculares",
                   url: "RecursosCurriculares/Home/Ejes/{sectorId}",
                   defaults: new { area = "RecursosCurriculares", controller = "Home", action = "Ejes" }
               );

            context.MapRoute(
                   name: "AddEjeRecursoCurricular",
                   url: "RecursosCurriculares/Eje/AddEje/{sectorId}",
                   defaults: new { area = "RecursosCurriculares", controller = "Eje", action = "AddEje" }
               );

            context.MapRoute(
                   name: "EditEjeRecursoCurricular",
                   url: "RecursosCurriculares/Eje/EditEje/{sectorId}/{id}",
                   defaults: new { area = "RecursosCurriculares", controller = "Eje", action = "EditEje" }
               );

            context.MapRoute(
                   name: "DeleteEjeRecursoCurricular",
                   url: "RecursosCurriculares/Eje/DeleteEje/{sectorId}/{id}",
                   defaults: new { area = "RecursosCurriculares", controller = "Eje", action = "DeleteEje" }
               );

            #endregion

            #region Contenido

            context.MapRoute(
                   name: "GetContenidos",
                   url: "RecursosCurriculares/Contenido/GetContenidos/{tipoEducacionCodigo}/{gradoCodigo}/{sectorId}/{ejeId}",
                   defaults: new { area = "RecursosCurriculares", controller = "Contenido", action = "GetContenidos" }
               );

            context.MapRoute(
                   name: "AddContenido",
                   url: "RecursosCurriculares/Contenido/AddContenido/{tipoEducacionCodigo}/{gradoCodigo}/{sectorId}/{ejeId}",
                   defaults: new { area = "RecursosCurriculares", controller = "Contenido", action = "AddContenido" }
               );

            context.MapRoute(
                   name: "EditContenido",
                   url: "RecursosCurriculares/Contenido/EditContenido/{tipoEducacionCodigo}/{gradoCodigo}/{sectorId}/{ejeId}/{Id}",
                   defaults: new { area = "RecursosCurriculares", controller = "Contenido", action = "EditContenido" }
               );

            context.MapRoute(
                   name: "DeleteContenido",
                   url: "RecursosCurriculares/Contenido/DeleteContenido/{tipoEducacionCodigo}/{gradoCodigo}/{sectorId}/{ejeId}/{Id}",
                   defaults: new { area = "RecursosCurriculares", controller = "Contenido", action = "DeleteContenido" }
               );

            #endregion

            #region ObjetivoVertical

            context.MapRoute(
                   name: "GetObjetivoVertical",
                   url: "RecursosCurriculares/ObjetivoVertical/GetObjetivosVerticales/{tipoEducacionCodigo}/{GradoCodigo}/{sectorId}",
                   defaults: new { area = "RecursosCurriculares", controller = "ObjetivoVertical", action = "GetObjetivosVerticales" }
               );

            context.MapRoute(
                   name: "AddObjetivoVertical",
                   url: "RecursosCurriculares/ObjetivoVertical/AddObjetivoVertical/{tipoEducacionCodigo}/{GradoCodigo}/{sectorId}",
                   defaults: new { area = "RecursosCurriculares", controller = "ObjetivoVertical", action = "AddObjetivoVertical" }
               );

            context.MapRoute(
                   name: "EditObjetivoVertical",
                   url: "RecursosCurriculares/ObjetivoVertical/EditObjetivoVertical/{tipoEducacionCodigo}/{GradoCodigo}/{sectorId}/{id}",
                   defaults: new { area = "RecursosCurriculares", controller = "ObjetivoVertical", action = "EditObjetivoVertical" }
               );

            context.MapRoute(
                   name: "DeleteObjetivoVertical",
                   url: "RecursosCurriculares/ObjetivoVertical/DeleteObjetivoVertical/{tipoEducacionCodigo}/{GradoCodigo}/{sectorId}/{id}",
                   defaults: new { area = "RecursosCurriculares", controller = "ObjetivoVertical", action = "DeleteObjetivoVertical" }
               );

            #endregion

            #region AprendizajeEsperado

            context.MapRoute(
                   name: "GetAprendizajeEsperado",
                   url: "RecursosCurriculares/AprendizajeEsperado/GetAprendizajesEsperados/{tipoEducacionCodigo}/{GradoCodigo}/{sectorId}",
                   defaults: new { area = "RecursosCurriculares", controller = "AprendizajeEsperado", action = "GetAprendizajesEsperados" }
               );

            context.MapRoute(
                   name: "GetAprendizajeIndicador",
                   url: "RecursosCurriculares/AprendizajeEsperado/GetAprendizajesIndicadores/{tipoEducacionCodigo}/{GradoCodigo}/{sectorId}/{aprendizajeId}",
                   defaults: new { area = "RecursosCurriculares", controller = "AprendizajeEsperado", action = "GetAprendizajesIndicadores" }
               );

            context.MapRoute(
                   name: "AddAprendizaje",
                   url: "RecursosCurriculares/AprendizajeEsperado/AddAprendizaje/{tipoEducacionCodigo}/{GradoCodigo}/{sectorId}",
                   defaults: new { area = "RecursosCurriculares", controller = "AprendizajeEsperado", action = "AddAprendizaje" }
               );

            context.MapRoute(
                   name: "EditAprendizaje",
                   url: "RecursosCurriculares/AprendizajeEsperado/EditAprendizaje/{tipoEducacionCodigo}/{GradoCodigo}/{sectorId}/{aprendizajeId}",
                   defaults: new { area = "RecursosCurriculares", controller = "AprendizajeEsperado", action = "EditAprendizaje" }
               );

            context.MapRoute(
                   name: "DeleteAprendizaje",
                   url: "RecursosCurriculares/AprendizajeEsperado/DeleteAprendizaje/{tipoEducacionCodigo}/{GradoCodigo}/{sectorId}/{aprendizajeId}",
                   defaults: new { area = "RecursosCurriculares", controller = "AprendizajeEsperado", action = "DeleteAprendizaje" }
               );

            context.MapRoute(
                   name: "GetAprendizajeContenidos",
                   url: "RecursosCurriculares/AprendizajeEsperado/GetAprendizajeContenidos/{tipoEducacionCodigo}/{GradoCodigo}/{sectorId}/{aprendizajeId}",
                   defaults: new { area = "RecursosCurriculares", controller = "AprendizajeEsperado", action = "GetAprendizajeContenidos" }
               );

            context.MapRoute(
                   name: "GetAprendizajeObjetivosVerticales",
                   url: "RecursosCurriculares/AprendizajeEsperado/GetAprendizajeObjetivosVerticales/{tipoEducacionCodigo}/{GradoCodigo}/{sectorId}/{aprendizajeId}",
                   defaults: new { area = "RecursosCurriculares", controller = "AprendizajeEsperado", action = "GetAprendizajeObjetivosVerticales" }
               );

            context.MapRoute(
                   name: "AddAprendizajeIndicador",
                   url: "RecursosCurriculares/AprendizajeEsperado/AddIndicador/{tipoEducacionCodigo}/{GradoCodigo}/{sectorId}/{aprendizajeId}",
                   defaults: new { area = "RecursosCurriculares", controller = "AprendizajeEsperado", action = "AddIndicador" }
               );

            context.MapRoute(
                   name: "EditIndicador",
                   url: "RecursosCurriculares/AprendizajeEsperado/EditIndicador/{tipoEducacionCodigo}/{GradoCodigo}/{sectorId}/{aprendizajeId}/{indicadorId}",
                   defaults: new { area = "RecursosCurriculares", controller = "AprendizajeEsperado", action = "EditIndicador" }
               );

            context.MapRoute(
                   name: "DeleteIndicador",
                   url: "RecursosCurriculares/AprendizajeEsperado/DeleteIndicador/{tipoEducacionCodigo}/{GradoCodigo}/{sectorId}/{aprendizajeId}/{indicadorId}",
                   defaults: new { area = "RecursosCurriculares", controller = "AprendizajeEsperado", action = "DeleteIndicador" }
               );

            #endregion

            #region Unidad

            context.MapRoute(
                   name: "GetUnidadesRecursoCurricular",
                   url: "RecursosCurriculares/Unidad/GetUnidades/{tipoEducacionCodigo}/{gradoCodigo}/{sectorId}",
                   defaults: new { area = "RecursosCurriculares", controller = "Unidad", action = "GetUnidades" }
               );

            context.MapRoute(
                   name: "GetAddUnidadRecursoCurricular",
                   url: "RecursosCurriculares/Unidad/AddUnidad/{tipoEducacionCodigo}/{gradoCodigo}/{sectorId}",
                   defaults: new { area = "RecursosCurriculares", controller = "Unidad", action = "AddUnidad" }
               );

            context.MapRoute(
                   name: "GetEditUnidadRecursoCurricular",
                   url: "RecursosCurriculares/Unidad/EditUnidad/{tipoEducacionCodigo}/{gradoCodigo}/{sectorId}/{unidadId}",
                   defaults: new { area = "RecursosCurriculares", controller = "Unidad", action = "EditUnidad" }
               );

            context.MapRoute(
                   name: "GetDeleteUnidadRecursoCurricular",
                   url: "RecursosCurriculares/Unidad/DeleteUnidad/{tipoEducacionCodigo}/{gradoCodigo}/{sectorId}/{unidadId}",
                   defaults: new { area = "RecursosCurriculares", controller = "Unidad", action = "DeleteUnidad" }
               );

            context.MapRoute(
                   name: "GetUnidadHabilidadesRecursoCurricular",
                   url: "RecursosCurriculares/Unidad/GetAprendizajes/{unidadId}/{tipoEducacionCodigo}/{gradoCodigo}/{sectorId}",
                   defaults: new { area = "RecursosCurriculares", controller = "Unidad", action = "GetAprendizajes" }
               );

            context.MapRoute(
                   name: "GetUnidadesComboRecursosCurriculares",
                   url: "RecursosCurriculares/Home/Unidades/{tipoEducacionCodigo}/{gradoCodigo}/{sectorId}",
                   defaults: new { area = "RecursosCurriculares", controller = "Home", action = "Unidades" }
               );

            #endregion

            #region ObjetivoTransversal

            context.MapRoute(
                   name: "GetObjetivoTransversales",
                   url: "RecursosCurriculares/ObjetivoTransversal/GetObjetivoTransversales/{tipoEducacionCodigo}/{GradoCodigo}/{sectorId}/{unidadId}",
                   defaults: new { area = "RecursosCurriculares", controller = "ObjetivoTransversal", action = "GetObjetivoTransversales" }
               );

            context.MapRoute(
                   name: "AddObjetivoTransversal",
                   url: "RecursosCurriculares/ObjetivoTransversal/AddObjetivoTransversal/{tipoEducacionCodigo}/{GradoCodigo}/{sectorId}/{unidadId}",
                   defaults: new { area = "RecursosCurriculares", controller = "ObjetivoTransversal", action = "AddObjetivoTransversal" }
               );

            context.MapRoute(
                   name: "EditObjetivoTransversal",
                   url: "RecursosCurriculares/ObjetivoTransversal/EditObjetivoTransversal/{tipoEducacionCodigo}/{GradoCodigo}/{sectorId}/{unidadId}/{objetivoTransversalId}",
                   defaults: new { area = "RecursosCurriculares", controller = "ObjetivoTransversal", action = "EditObjetivoTransversal" }
               );

            context.MapRoute(
                   name: "DeleteObjetivoTransversal",
                   url: "RecursosCurriculares/ObjetivoTransversal/DeleteObjetivoTransversal/{tipoEducacionCodigo}/{GradoCodigo}/{sectorId}/{unidadId}/{objetivoTransversalId}",
                   defaults: new { area = "RecursosCurriculares", controller = "ObjetivoTransversal", action = "DeleteObjetivoTransversal" }
               );

            context.MapRoute(
                   name: "AddObjetivoTransversalIndicador",
                   url: "RecursosCurriculares/ObjetivoTransversal/AddObjetivoTransversalIndicador/{tipoEducacionCodigo}/{GradoCodigo}/{sectorId}/{unidadId}/{objetivoTransversalId}",
                   defaults: new { area = "RecursosCurriculares", controller = "ObjetivoTransversal", action = "AddObjetivoTransversalIndicador" }
               );

            context.MapRoute(
                   name: "EditObjetivoTransversalIndicador",
                   url: "RecursosCurriculares/ObjetivoTransversal/EditObjetivoTransversalIndicador/{tipoEducacionCodigo}/{GradoCodigo}/{sectorId}/{unidadId}/{objetivoTransversalId}/{indicadorId}",
                   defaults: new { area = "RecursosCurriculares", controller = "ObjetivoTransversal", action = "EditObjetivoTransversalIndicador" }
               );

            context.MapRoute(
                   name: "DeleteObjetivoTransversalIndicador",
                   url: "RecursosCurriculares/ObjetivoTransversal/DeleteObjetivoTransversalIndicador/{tipoEducacionCodigo}/{GradoCodigo}/{sectorId}/{unidadId}/{objetivoTransversalId}/{indicadorId}",
                   defaults: new { area = "RecursosCurriculares", controller = "ObjetivoTransversal", action = "DeleteObjetivoTransversalIndicador" }
               );

            context.MapRoute(
                   name: "GetObjetivoTransversalIndicadores",
                   url: "RecursosCurriculares/ObjetivoTransversal/GetObjetivoTransversalIndicadores/{tipoEducacionCodigo}/{GradoCodigo}/{sectorId}/{unidadId}/{objetivoTransversalId}",
                   defaults: new { area = "RecursosCurriculares", controller = "ObjetivoTransversal", action = "GetObjetivoTransversalIndicadores" }
               );

            #endregion
        }
    }
}